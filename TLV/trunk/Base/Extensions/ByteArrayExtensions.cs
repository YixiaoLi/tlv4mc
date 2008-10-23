using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
    /// <summary>
    /// Byteの配列から文字コードを検出する拡張メソッドを定義するクラス
    /// </summary>
    public static class ByteArrayExtensions
    {
        private static bool bBOM, bLE, bBE;
        private static int sjis, euc, utf8;

        /// <summary>
        /// Byteの配列から文字コードを検出する拡張メソッド
        /// </summary>
        /// <param name="bytes">検出対象のByte配列</param>
        /// <returns>文字コード</returns>
        public static Encoding GetCode(this byte[] bytes)
        {

            if (IsUTF16(bytes) == true)
            {
                if (bLE == true)
                    return Encoding.Unicode;

                else if (bBE == true)
                    return Encoding.BigEndianUnicode;
            }

            else if (IsJis(bytes) == true)
                return Encoding.GetEncoding(50220);

            else if (IsAscii(bytes) == true)
                return Encoding.ASCII;

            else
            {
                bool bUTF8 = IsUTF8(bytes);
                bool bShitJis = IsShiftJis(bytes);
                bool bEUC = IsEUC(bytes);

                if (bUTF8 == true || bShitJis == true || bEUC == true)
                {
                    if (euc > sjis && euc > utf8)
                        return Encoding.GetEncoding(51932);
                    else if (sjis > euc && sjis > utf8)
                        return Encoding.GetEncoding(932);
                    else if (utf8 > euc && utf8 > sjis)
                        return Encoding.UTF8;
                }
                else
                {
                    return null;
                }
            }

            return null;
        }

        private static bool IsUTF16(byte[] bytes)      // Check for UTF-16
        {
            int len = bytes.Length;
            byte b1, b2;
            bLE = false; bBE = false;

            if (len >= 2)
            {
                b1 = bytes[0];
                b2 = bytes[1];

                if (b1 == 0xFF && b2 == 0xFE)
                {
                    bLE = true;
                    return true;
                }
                else if (b1 == 0xFE && b2 == 0xFF)
                {
                    bBE = true;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private static bool IsJis(byte[] bytes)        // Check for JIS (ISO-2022-JP)
        {
            int len = bytes.Length;
            byte b1, b2, b3, b4, b5, b6;

            for (int i = 0; i < len; i++)
            {
                b1 = bytes[i];

                if (b1 > 0x7F)
                {
                    return false;   // Not ISO-2022-JP (0x00～0x7F)
                }
                else if (i < len - 2)
                {
                    b2 = bytes[i + 1]; b3 = bytes[i + 2];

                    if (b1 == 0x1B && b2 == 0x28 && b3 == 0x42)
                        return true;    // ESC ( B  : JIS ASCII

                    else if (b1 == 0x1B && b2 == 0x28 && b3 == 0x4A)
                        return true;    // ESC ( J  : JIS X 0201-1976 Roman Set

                    else if (b1 == 0x1B && b2 == 0x28 && b3 == 0x49)
                        return true;    // ESC ( I  : JIS X 0201-1976 kana

                    else if (b1 == 0x1B && b2 == 0x24 && b3 == 0x40)
                        return true;    // ESC $ @  : JIS X 0208-1978(old_JIS)

                    else if (b1 == 0x1B && b2 == 0x24 && b3 == 0x42)
                        return true;    // ESC $ B  : JIS X 0208-1983(new_JIS)
                }
                else if (i < len - 3)
                {
                    b2 = bytes[i + 1]; b3 = bytes[i + 2]; b4 = bytes[i + 3];

                    if (b1 == 0x1B && b2 == 0x24 && b3 == 0x28 && b4 == 0x44)
                        return true;    // ESC $ ( D  : JIS X 0212-1990（JIS_hojo_kanji）
                }
                else if (i < len - 5)
                {
                    b2 = bytes[i + 1]; b3 = bytes[i + 2];
                    b4 = bytes[i + 3]; b5 = bytes[i + 4]; b6 = bytes[i + 5];

                    if (b1 == 0x1B && b2 == 0x26 && b3 == 0x40 &&
                         b4 == 0x1B && b5 == 0x24 && b6 == 0x42)
                    {
                        return true;    // ESC & @ ESC $ B  : JIS X 0208-1990
                    }
                }
            }

            return false;
        }

        private static bool IsAscii(byte[] bytes)      // Check for Ascii
        {
            int len = bytes.Length;

            for (int i = 0; i < len; i++)
            {
                if (bytes[i] <= 0x7F)
                {
                    // ASCII : 0x00～0x7F
                    ;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsShiftJis(byte[] bytes)   // Check for Shift-JIS
        {
            int len = bytes.Length;
            byte b1, b2;
            sjis = 0;

            for (int i = 0; i < len; i++)
            {
                b1 = bytes[i];

                if ((b1 <= 0x7F) || (b1 >= 0xA1 && b1 <= 0xDF))
                {
                    // ASCII : 0x00～0x7F
                    // kana  : 0xA1～0xDF
                    ;
                }
                else if (i < len - 1)
                {
                    b2 = bytes[i + 1];

                    if (
                        ((b1 >= 0x81 && b1 <= 0x9F) || (b1 >= 0xE0 && b1 <= 0xFC)) &&
                        ((b2 >= 0x40 && b2 <= 0x7E) || (b2 >= 0x80 && b2 <= 0xFC))
                        )
                    {
                        // kanji first byte  : 0x81～0x9F、0xE0～0xFC
                        //       second byte : 0x40～0x7E、0x80～0xFC
                        i++;
                        sjis += 2;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static bool IsEUC(byte[] bytes)        // Check for euc-jp 
        {
            int len = bytes.Length;
            byte b1, b2, b3;
            euc = 0;

            for (int i = 0; i < len; i++)
            {
                b1 = bytes[i];

                if (b1 <= 0x7F)
                {   //  ASCII : 0x00～0x7F
                    ;
                }
                else if (i < len - 1)
                {
                    b2 = bytes[i + 1];

                    if ((b1 >= 0xA1 && b1 <= 0xFE) && (b2 >= 0xA1 && b1 <= 0xFE))
                    { // kanji - first & second byte : 0xA1～0xFE
                        i++;
                        euc += 2;
                    }
                    else if ((b1 == 0x8E) && (b2 >= 0xA1 && b1 <= 0xDF))
                    { // kana - first byte : 0x8E, second byte : 0xA1～0xDF
                        i++;
                        euc += 2;
                    }
                    else if (i < len - 2)
                    {
                        b3 = bytes[i + 2];

                        if ((b1 == 0x8F) &&
                            (b2 >= 0xA1 && b2 <= 0xFE) && (b3 >= 0xA1 && b3 <= 0xFE))
                        { // hojo kanji - first byte : 0x8F, second & third byte : 0xA1～0xFE
                            i += 2;
                            euc += 3;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static bool IsUTF8(byte[] bytes)       // Check for UTF-8
        {
            int len = bytes.Length;
            byte b1, b2, b3, b4;
            utf8 = 0; bBOM = false;

            for (int i = 0; i < len; i++)
            {
                b1 = bytes[i];

                if (b1 <= 0x7F)
                { //  ASCII : 0x00～0x7F
                    ;
                }
                else if (i < len - 1)
                {
                    b2 = bytes[i + 1];

                    if ((b1 >= 0xC0 && b1 <= 0xDF) &&
                        (b2 >= 0x80 && b2 <= 0xBF))
                    { // 2 byte char
                        i += 1;
                        utf8 += 2;
                    }
                    else if (i < len - 2)
                    {
                        b3 = bytes[i + 2];

                        if (b1 == 0xEF && b2 == 0xBB && b3 == 0xBF)
                        { // BOM : 0xEF 0xBB 0xBF
                            bBOM = true;
                            i += 2;
                            utf8 += 3;
                        }
                        else if ((b1 >= 0xE0 && b1 <= 0xEF) &&
                            (b2 >= 0x80 && b2 <= 0xBF) &&
                            (b3 >= 0x80 && b2 <= 0xBF))
                        { // 3 byte char
                            i += 2;
                            utf8 += 3;
                        }

                        else if (i < len - 3)
                        {
                            b4 = bytes[i + 3];

                            if ((b1 >= 0xF0 && b1 <= 0xF7) &&
                                (b2 >= 0x80 && b2 <= 0xBF) &&
                                (b3 >= 0x80 && b2 <= 0xBF) &&
                                (b4 >= 0x80 && b2 <= 0xBF))
                            { // 4 byte char
                                i += 3;
                                utf8 += 4;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }


        public static string GetString(this byte[] bytes)
        {
            return bytes.GetCode().GetString(bytes);
        }
    }
}
