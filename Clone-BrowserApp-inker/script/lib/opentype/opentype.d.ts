declare module opentype {

    /**
     * Asynchronously load the font from a URL or a filesystem. When done, call the callback
     * with two arguments `(err, font)`. The `err` will be null on success,
     * the `font` is a Font object.
     */
    function load(url: string, callback: (err: Error, font: Font) => void);

    /**
     * Parse the OpenType file data (as an ArrayBuffer) and return a Font object.
     * If the file could not be parsed (most likely because it contains Postscript outlines)
     * we return an empty Font object with the `supported` flag set to `false`.
     */
    function parse(buffer: ArrayBuffer): Font;

    /**
     * Represents a loaded OpenType font file.
     * Contains a set of glyphs and methods to draw text on a drawing context, or to get a path representing the text.
     */
    class Font {
        constructor(options);

        /**
         * Create a Path that represents the given text.
         * @param x Horizontal position of the beginning of the text. (default: 0)
         * @param y Vertical position of the baseline of the text. (default: 0)
         * @param fontSize Size of the text in pixels (default: 72).
         * @param options Object containing: kerning: if true takes kerning information into account (default: true)
         */
        getPath(text: string, x: number, y: number, fontSize: number, options?: { kerning: boolean; }): Path;

        familyName: string;
        styleName: string;
        supported: boolean;
    }

    //class Glyph {
    //    constructor();
    //}

    /**
     * A bézier path containing a set of path commands similar to a SVG path.
     */
    class Path {
        constructor();
        commands: Command[];
        fill: string;
        stroke: string;
        strokeWidth: string;
    }

    /**
     * Path command.
     */
    interface Command {
        type: string;
        x?: number;
        y?: number;
        x1?: number;
        y1?: number;
        x2?: number;
        y2?: number;
    }
}