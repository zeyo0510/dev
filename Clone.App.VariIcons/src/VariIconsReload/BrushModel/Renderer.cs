using System;
using System.Drawing;
using System.Threading;
using System.Drawing.Drawing2D;
using VariIconsSDK.Model;

namespace VariIconsReload.BrushModel
{
	/// <summary>
	/// async worker rendering an image
	/// </summary>
	public abstract class ImageRenderer : Worker<bool, bool>, IDisposable
	{
		#region types
		//used to add new chunks
		private class ImageProgressMessage
		{
			public ImageProgressMessage(Bitmap chunk, Point location)
			{
				Chunk = chunk;
				_location = location;
			}
			public Bitmap Chunk;
			public Point _location;
		}
		#endregion
		#region variables
		private Bitmap _buffer;
		//core side
		private Size _size;
		private int _chunksize;
		#endregion
		/// <summary>
		/// initializes the imagerenderer using the given buffer image
		/// </summary>
		/// <param name="buffer"></param>
		public ImageRenderer(Bitmap buffer)
			: this(buffer, 1) { }
		public ImageRenderer(Bitmap buffer, int chunksize)
		{
			if (buffer == null)
				throw new ArgumentNullException("buffer");
			if (chunksize < 1)
				throw new ArgumentOutOfRangeException("chunksize");
			_buffer = buffer;
			//can do this cross-thread reference on value types
			_size = buffer.Size;
			_chunksize = chunksize;
		}
		public virtual void Dispose()
		{
			Cancel();
			Join();
		}
		#region controller
		//handle data posted from processing thread
		protected override void HandleMonitorMessage(object message)
		{
			ImageProgressMessage args;
			if ((args = message as ImageProgressMessage) != null)
			{
				if (_buffer != null)
				{
					//copy new chunk to buffer
					using (Graphics gr = Graphics.FromImage(_buffer))
					{
						gr.CompositingMode = CompositingMode.SourceCopy;
						gr.DrawImageUnscaled(args.Chunk, args._location);
					}
					//post progress feedback
					base.HandleMonitorMessage(new ProgressEventArgs(
						args._location.Y, _buffer.Height));
				}
				args.Chunk.Dispose();
			}
			else
				base.HandleMonitorMessage(message);
		}
		//divide in chunks and process
		protected override bool Run(bool argument)
		{
			BeginRendering();
			for (int y = 0; y < _size.Height; y += _chunksize)
			{
				if (CancelPending)
				{
					EndRendering(true);
					return false;
				}
				Bitmap chunk = new Bitmap(_size.Width,
					Math.Min(_size.Height - y, _chunksize));

				RenderChunk(chunk, new Point(0, y));
				PostToMonitor(new ImageProgressMessage(chunk, new Point(0, y)));
			}
			EndRendering(false);
			return true;
		}
		#endregion
		#region virtual
		protected Size Size
		{
			get { return _size; }
		}
		/// <summary>
		/// called before chunks are rendered
		/// </summary>
		protected virtual void BeginRendering()
		{
		}
		protected virtual void EndRendering(bool cancelled)
		{
		}
		/// <summary>
		/// override this to render a chunk
		/// </summary>
		protected abstract void RenderChunk(Bitmap chunk, Point location);
		#endregion
		public override void Start(bool argument)
		{
			if (_buffer == null)
				return;
			base.Start(argument);
		}
		/// <summary>
		/// gets the buffer given at construction time.
		/// </summary>
		public Bitmap Buffer
		{
			get { return _buffer; }
		}
	}
	/// <summary>
	/// a random colored line generator
	/// </summary>
	public class HoaxImageRenderer : ImageRenderer
	{
		Random rnd;
		public HoaxImageRenderer(Bitmap bmp) : base(bmp) { }
		protected override void BeginRendering()
		{
			rnd = new Random();
		}
		protected override void RenderChunk(Bitmap chunk, Point location)
		{
			using (Graphics gr = Graphics.FromImage(chunk))
			{
				gr.Clear(Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255)));
				Thread.Sleep(20);
			}
		}
	}
}
