using System;
using OpenTK.Graphics.OpenGL;
using System.Runtime.InteropServices;

using System.Collections.Generic;
using CoreText;
using FontRef = System.UInt16;

namespace XiEditor
{
    public class FontCache
    {
        public readonly List<CachedFont> Fonts = new List<CachedFont>();
        public readonly Dictionary<CTFont, FontRef> FontMap = new Dictionary<CTFont, FontRef>();

        public FontRef GetFontRefId(CTFont font)
        {
            if(FontMap.TryGetValue(font, out FontRef refId))
            {
                return refId;
            }
            else
            {
                var nextCachedFontRefId = Convert.ToUInt16(Fonts.Count);
                Fonts.Add(new CachedFont(font));
                FontMap.Add(font, nextCachedFontRefId);

                return nextCachedFontRefId;
            }
        }

        public void Flush()
        {

            FontMap.Clear();
        }
    }

    public class CachedFont
    {
        public readonly CTFont Font;
        public readonly Dictionary<uint, CachedGlyph> Map =
            new Dictionary<uint, CachedGlyph>();

        public CachedFont(CTFont font)
        {
            Font = font;
        }
    }

    public struct CachedGlyph
    {

    }

    public class Atlas
    {
        readonly uint textureId;
        readonly float uvScale;
        readonly int width;
        readonly int height;

        /// <summary>
        /// Create a texture Atlas (see wikipedia)
        /// </summary>
        public Atlas()
        {
            GL.GenTextures(1, out uint id);
            textureId = id;

            GL.BindTexture(TextureTarget.Texture2D, textureId);
            GL.PixelStore(PixelStoreParameter.UnpackAlignment, 1);

            GL.TexParameter(TextureTarget.ProxyTexture2D, TextureParameterName.TextureWrapS, (int)All.ClampToEdge);
            GL.TexParameter(TextureTarget.ProxyTexture2D, TextureParameterName.TextureWrapT, (int)All.ClampToEdge);

            GL.TexParameter(TextureTarget.ProxyTexture2D, TextureParameterName.TextureMagFilter, (int)All.Nearest);
            GL.TexParameter(TextureTarget.ProxyTexture2D, TextureParameterName.TextureMinFilter, (int)All.Nearest);

            //4 ints, one each for rgb&a, times width and height
            var byteCount = (width * height * 4);
            IntPtr data = Marshal.AllocHGlobal(byteCount * IntPtr.Size);

            GL.TexImage2D(
                TextureTarget.Texture2D,
                level: 0,
                PixelInternalFormat.Rgba,
                width,
                height, 0,
                PixelFormat.Bgra,
                PixelType.UnsignedByte, data);

            uvScale = 1.0f / width;

        }

        // Round a value up to the next power-of-2 multiple of a number at most fracBits long
        public int RoundUp(int y, int fracBits)
        {
            if(y < (1 << fracBits))
            {
                return y;
            }


            //todo
            throw new NotImplementedException();
        }

        public CachedGlyph? GetGlyph()//todo signature
        {
            //todo
            throw new NotImplementedException();
        }

        public void FlushCache()
        {
            //todo
            throw new NotImplementedException();
        }
    }
}
