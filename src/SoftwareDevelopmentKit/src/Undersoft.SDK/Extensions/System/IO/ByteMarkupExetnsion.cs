
// <copyright file="ByteMarkupExetnsion.cs" company="UltimatR.Core">
//     Copyright (c) Undersoft. All rights reserved.
// </copyright>



/// <summary>
/// The IO namespace.
/// </summary>
namespace System.IO
{



    /// <summary>
    /// Class ByteMarkupExtension.
    /// </summary>
    public static class ByteMarkupExtension
    {
        #region Methods







        /// <summary>
        /// Determines whether the specified noisekind is markup.
        /// </summary>
        /// <param name="checknoise">The checknoise.</param>
        /// <param name="noisekind">The noisekind.</param>
        /// <returns><c>true</c> if the specified noisekind is markup; otherwise, <c>false</c>.</returns>
        public static bool IsMarkup(this byte checknoise, out MarkupType noisekind)
        {
            switch (checknoise)
            {
                case (byte)MarkupType.Block:
                    noisekind = MarkupType.Block;
                    return true;
                case (byte)MarkupType.End:
                    noisekind = MarkupType.End;
                    return true;
                case (byte)MarkupType.Empty:
                    noisekind = MarkupType.Empty;
                    return false;
                default:
                    noisekind = MarkupType.None;
                    return false;
            }
        }







        /// <summary>
        /// Determines whether the specified spliterkind is spliter.
        /// </summary>
        /// <param name="checknoise">The checknoise.</param>
        /// <param name="spliterkind">The spliterkind.</param>
        /// <returns><c>true</c> if the specified spliterkind is spliter; otherwise, <c>false</c>.</returns>
        public static bool IsSpliter(this byte checknoise, out MarkupType spliterkind)
        {
            switch (checknoise)
            {
                case (byte)MarkupType.Empty:
                    spliterkind = MarkupType.Empty;
                    return true;
                case (byte)MarkupType.Line:
                    spliterkind = MarkupType.Line;
                    return true;
                case (byte)MarkupType.Space:
                    spliterkind = MarkupType.Space;
                    return true;
                case (byte)MarkupType.Semi:
                    spliterkind = MarkupType.Semi;
                    return true;
                case (byte)MarkupType.Coma:
                    spliterkind = MarkupType.Coma;
                    return true;
                case (byte)MarkupType.Colon:
                    spliterkind = MarkupType.Colon;
                    return true;
                case (byte)MarkupType.Dot:
                    spliterkind = MarkupType.Dot;
                    return true;
                default:
                    spliterkind = MarkupType.None;
                    return false;
            }
        }

        #endregion
    }
}
