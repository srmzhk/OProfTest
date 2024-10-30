using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OProfTest.MVVM.Model;

namespace OProfTest.Repositories
{
    public class TestsImagesRepository
    {
        private readonly ModelsManager _dbManager;
        public TestsImagesRepository()
        {
            _dbManager = new ModelsManager();
        }
        public void AddImageByTestId(string path, int testId)
        {
            if (path.Length > 0)
            {
                byte[] buff;
                if (File.Exists(path))
                {
                    buff = File.ReadAllBytes(path);
                    Image img = Image.FromFile(path);
                    Bitmap resizedImage = new Bitmap(img, new System.Drawing.Size(256, 256));
                    using (var stream = new MemoryStream())
                    {
                        resizedImage.Save(stream, ImageFormat.Jpeg);
                        byte[] bytes = stream.ToArray();
                        TestImage TestImage = new TestImage();
                        TestImage.FileExtension = Path.GetExtension(path);
                        TestImage.Image = bytes;
                        TestImage.Size = bytes.Length;
                        TestImage.TestID = testId;
                        TestImage.FilePath = path;
                        _dbManager.TestImages.Add(TestImage);
                    }
                }
                _dbManager.SaveChanges();
            }
        }
        public void DeleteImageByTestId(int TestId)
        {
            var img = _dbManager.TestImages.Where(i => i.TestID == TestId).First();
            _dbManager.TestImages.Remove(img);
            _dbManager.SaveChanges();
        }

        public TestImage GetImageByTestId(int TestId)
        {
            return _dbManager.TestImages.Where(img => img.TestID == TestId).FirstOrDefault();
        }

        public void UpdateImageByTestId(int TestId, TestImage image)
        {
            var findImage = _dbManager.TestImages.Where(img => img.TestID == TestId).FirstOrDefault();
            if (findImage != null)
            {
                _dbManager.TestImages.Remove(findImage);
                _dbManager.TestImages.Add(image);
                _dbManager.SaveChanges();
            }
        }
        public TestImage ReturnNewTestImage(string path, int testId)
        {
            if (path.Length > 0)
            {
                TestImage TestImage = new TestImage();
                byte[] buff;
                if (File.Exists(path))
                {
                    buff = File.ReadAllBytes(path);
                    Image img = Image.FromFile(path);
                    Bitmap resizedImage = new Bitmap(img, new System.Drawing.Size(256, 256));
                    using (var stream = new MemoryStream())
                    {
                        resizedImage.Save(stream, ImageFormat.Jpeg);
                        byte[] bytes = stream.ToArray();
                        TestImage.FileExtension = Path.GetExtension(path);
                        TestImage.Image = bytes;
                        TestImage.Size = bytes.Length;
                        TestImage.TestID = testId;
                        TestImage.FilePath = path;
                    }
                }
                return TestImage;
            }
            else
                return null;
        }
    }
}
