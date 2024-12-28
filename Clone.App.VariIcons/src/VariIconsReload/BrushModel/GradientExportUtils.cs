using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using Microsoft.CSharp;

namespace VariIconsReload.BrushModel
{
	public class GradientExportUtils
	{
		private GradientExportUtils() { }

		/// <summary>
		/// create an export string by codedom and codegenerator
		/// </summary>
		public static string ExportCode(ColorBlend blend)
		{
			if (blend == null)
				throw new ArgumentNullException("blend");
			StringBuilder ret = new StringBuilder();
			using (StringWriter w = new StringWriter(ret))
			{
				CodeGeneratorOptions options = new CodeGeneratorOptions();
				using (CSharpCodeProvider generator = new CSharpCodeProvider())
				{
					CodeExpression[] parameters = new CodeExpression[0];
					//create colorblnd
					generator.GenerateCodeFromStatement(new CodeVariableDeclarationStatement(
						typeof(ColorBlend), "colorblnd",
						new CodeObjectCreateExpression(typeof(ColorBlend), parameters)), w, options);
					CodeVariableReferenceExpression colorblnd = new CodeVariableReferenceExpression("colorblnd");
					//create positions array
					CodePrimitiveExpression[] positions = new CodePrimitiveExpression[blend.Positions.Length];
					for (int k = 0; k < positions.Length; k++)
					{
						positions[k] = new CodePrimitiveExpression(blend.Positions[k]);
					}
					generator.GenerateCodeFromStatement(new CodeAssignStatement(
						new CodePropertyReferenceExpression(colorblnd, "Positions"),
						new CodeArrayCreateExpression(typeof(float), positions)), w, options);
					//create colors array
					CodeMethodInvokeExpression[] colors =
						new CodeMethodInvokeExpression[blend.Colors.Length];
					for (int m = 0; m < colors.Length; m++)
					{
						colors[m] = new CodeMethodInvokeExpression(
							new CodeTypeReferenceExpression(typeof(Color)), "FromArgb",
								new CodeExpression[] {
								new CodePrimitiveExpression(blend.Colors[m].A),
								new CodePrimitiveExpression(blend.Colors[m].R),
								new CodePrimitiveExpression(blend.Colors[m].G),
								new CodePrimitiveExpression(blend.Colors[m].B)});
					}
					generator.GenerateCodeFromStatement(new CodeAssignStatement(
						new CodePropertyReferenceExpression(colorblnd, "Colors"),
						new CodeArrayCreateExpression(typeof(Color), colors)), w, options);
				}
			}
			return ret.ToString();
		}

		/// <summary>
		/// export colorblend to a string list
		/// </summary>
		public static string ExportList(ColorBlend blend)
		{
			if (blend == null)
				throw new ArgumentNullException("blend");
			StringBuilder builder = new StringBuilder();
			for (int i = 0; i < blend.Positions.Length; i++)
			{
				builder.Append(blend.Colors[i].ToString());
				builder.Append(@" @ ");
				builder.Append(Math.Round(blend.Positions[i], 3));
				builder.Append("\r\n");
			}
			return builder.ToString();
		}
	}
}
