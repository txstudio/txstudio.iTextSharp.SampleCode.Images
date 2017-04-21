using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTextSharp.SampleCode.Images
{
    class Program
    {
        static void Main(string[] args)
        {
            var _srcPath = "../../Blizzard.pdf";
            var _buffer = File.ReadAllBytes(_srcPath);
            var _renderInstance = new ImageRenderListener();

            using(PdfReader _reader = new PdfReader(_buffer))
            {
                var _pageIndex = 0;
                var _pageTotal = _reader.NumberOfPages;

                PdfReaderContentParser _parser;

                while(_pageIndex < _pageTotal)
                {
                    _pageIndex = _pageIndex + 1;
                    _parser = new PdfReaderContentParser(_reader);

                    _renderInstance.PageIndex = _pageIndex;
                    _parser.ProcessContent(_pageIndex, _renderInstance);
                }
            }

            Console.WriteLine("press any key to exit");
            Console.ReadKey();
        }
    }

    /*
     * 實作 IRenderListener 介面
     *  取得 PDF 檔案的圖片資訊
     */
    public class ImageRenderListener : IRenderListener
    {
        private int _pageIndex;
        private int _imageIndex;

        public ImageRenderListener()
	    {
            this._pageIndex = 0;
            this._imageIndex = 0;
	    }

        public void BeginTextBlock() { }

        public void EndTextBlock() { }

        public void RenderText(TextRenderInfo renderInfo) { }

        public void RenderImage(ImageRenderInfo renderInfo)
        {
            var _image = renderInfo.GetImage();
            var _buffer = _image.GetImageAsBytes();

            Console.WriteLine("----------------------------");
            Console.WriteLine("取得圖片檔案");
            Console.WriteLine("頁碼\t{0}", this._pageIndex);
            Console.WriteLine("索引\t{0}", this._imageIndex);
            Console.WriteLine("Content-Length {0}", _buffer.Length);
            Console.WriteLine("----------------------------");
            Console.WriteLine();

            this._imageIndex = this._imageIndex + 1;
        }


        public int PageIndex
        {
            get
            {
                return this._pageIndex;
            }
            set
            {
                var _input = value;

                //當頁碼變更時，圖片索引重新計算
                if(_input == this._pageIndex)
                {

                }
                else
                {
                    this._pageIndex = _input;
                    this._imageIndex = 0;
                }
            }
        }

    }
}
