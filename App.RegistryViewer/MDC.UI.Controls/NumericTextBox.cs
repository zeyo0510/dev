using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace MDC.UI.Controls
{
	internal class NumericTextBox : TextBox
	{
		private char decimalSeparator;

		private char groupSeparator;

		private char negativeSign;

		private bool hexNumber;

		private bool allowDecimal;

		private bool allowGrouping;

		private bool allowNegative;

		private IContainer components;

		public override int MaxLength
		{
			get
			{
				return base.MaxLength;
			}
			set
			{
			}
		}

		public override bool ShortcutsEnabled
		{
			get
			{
				return base.ShortcutsEnabled;
			}
			set
			{
			}
		}

		public bool HexNumber
		{
			get
			{
				return hexNumber;
			}
			set
			{
				if (!(hexNumber ^ value))
				{
					return;
				}
				if (value)
				{
					if (AllowNegative)
					{
						Text = IntValue.ToString("x");
					}
					else
					{
						Text = UIntValue.ToString("x");
					}
					base.MaxLength = 8;
				}
				else
				{
					if (AllowNegative)
					{
						Text = IntValue.ToString();
					}
					else
					{
						Text = UIntValue.ToString();
					}
					base.MaxLength = 10;
				}
				hexNumber = value;
			}
		}

		public bool AllowDecimal
		{
			get
			{
				return allowDecimal;
			}
			set
			{
				allowDecimal = value;
				FilterText();
			}
		}

		public bool AllowGrouping
		{
			get
			{
				return allowGrouping;
			}
			set
			{
				allowGrouping = value;
				FilterText();
			}
		}

		public bool AllowNegative
		{
			get
			{
				return allowNegative;
			}
			set
			{
				allowNegative = value;
				FilterText();
			}
		}

		public int IntValue
		{
			get
			{
				try
				{
					if (Text == string.Empty)
					{
						return 0;
					}
					if (HexNumber)
					{
						return int.Parse(Text, NumberStyles.HexNumber);
					}
					return int.Parse(Text);
				}
				catch
				{
					return int.MaxValue;
				}
			}
		}

		public uint UIntValue
		{
			get
			{
				try
				{
					if (Text == string.Empty)
					{
						return 0u;
					}
					if (HexNumber)
					{
						return uint.Parse(Text, NumberStyles.HexNumber);
					}
					return uint.Parse(Text);
				}
				catch (Exception)
				{
					return uint.MaxValue;
				}
			}
		}

		public ulong ULongValue
		{
			get
			{
				try
				{
					if (Text == string.Empty)
					{
						return 0uL;
					}
					if (HexNumber)
					{
						return ulong.Parse(Text, NumberStyles.HexNumber);
					}
					return ulong.Parse(Text);
				}
				catch (Exception)
				{
					return ulong.MaxValue;
				}
			}
		}

		public float DecimalValue
		{
			get
			{
				try
				{
					if (Text == string.Empty)
					{
						return 0f;
					}
					if (HexNumber)
					{
						return float.Parse(Text, NumberStyles.HexNumber);
					}
					return float.Parse(Text);
				}
				catch
				{
					return float.NaN;
				}
			}
		}

		public NumericTextBox()
		{
			InitializeComponent();
			NumberFormatInfo numberFormat = CultureInfo.CurrentCulture.NumberFormat;
			decimalSeparator = numberFormat.NumberDecimalSeparator[0];
			groupSeparator = numberFormat.NumberGroupSeparator[0];
			negativeSign = numberFormat.NegativeSign[0];
		}

		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress(e);
			e.Handled = !IsDigit(e.KeyChar);
		}

		private bool IsDigit(char ch)
		{
			return char.IsControl(ch) || char.IsDigit(ch) || (HexNumber && char.IsLetter(ch) && char.ToLower(ch) <= 'f') || (AllowNegative && Text.Length == 0 && ch.Equals(negativeSign)) || (AllowGrouping && Text.Length > 0 && ch.Equals(groupSeparator)) || (AllowDecimal && ch.Equals(decimalSeparator) && Text.IndexOf(decimalSeparator) == -1);
		}

		private void FilterText()
		{
			StringBuilder stringBuilder = new StringBuilder(Text.Length);
			string text = Text;
			foreach (char c in text)
			{
				if (IsDigit(c))
				{
					stringBuilder.Append(c);
				}
			}
			if (stringBuilder.Length != Text.Length)
			{
				Text = stringBuilder.ToString();
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			base.SuspendLayout();
			this.MaxLength = 10;
			this.ShortcutsEnabled = false;
			base.ResumeLayout(false);
		}
	}
}
