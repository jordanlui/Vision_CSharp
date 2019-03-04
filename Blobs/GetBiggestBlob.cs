using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using SampleBase;

namespace SandboxCS
{
    class GetBiggestBlob : ISandbox
    {
        public void Run()
        {
            //string impath = "D:\\Projects\\Computer Vision\\images\\blob1.png";
            string impath = "D:\\Projects\\Computer Vision\\images\\blocks3_white.png";
            Mat gray = new Mat(impath, ImreadModes.Grayscale);

            // Blob the image
            OpenCvSharp.Blob.CvBlobs blobs = new OpenCvSharp.Blob.CvBlobs(gray);

            // Get the blob with biggest area
            int maxArea = 0;
            var biggestBlob = new OpenCvSharp.Blob.CvBlob();
            int indBiggest = 0;
            for (int i = 0; i < blobs.Count; i++)
            {
                if (blobs.ElementAt(i).Value.Area >= maxArea) 
                {
                    maxArea = blobs.ElementAt(i).Value.Area;
                    biggestBlob = blobs.ElementAt(i).Value;
                    indBiggest = i;
                }
            }

            // Make new image with only the big blob
            Mat imBigBlobGray = new Mat(gray.Size(), MatType.CV_8UC1);
            var aPoly = biggestBlob.Contour.ConvertToPolygon();

            // Render on image
            imBigBlobGray.FillConvexPoly(aPoly, Scalar.White);
            //Cv2.CvtColor(newIm, imBigBlobGray, ColorConversionCodes.BGR2GRAY);

            // Save to disk
            imBigBlobGray.SaveImage("largestBlob1Channel.png");

            Cv2.ImShow("input", gray);
            Cv2.ImShow("result", imBigBlobGray);

            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }
        
    }
}
