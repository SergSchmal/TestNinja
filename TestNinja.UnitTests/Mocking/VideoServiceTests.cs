using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class VideoServiceTests
    {
        [SetUp]
        public void SetUp()
        {
            _mockFileReader = new Mock<IFileReader>();
            _mockRepository = new Mock<IVideoRepository>();
            _videoService = new VideoService(_mockFileReader.Object, _mockRepository.Object);
        }

        private VideoService _videoService;
        private Mock<IFileReader> _mockFileReader;
        private Mock<IVideoRepository> _mockRepository;

        [Test]
        public void GetUnprocessedVideosAsCsv_AFewUnprocessedVideos_ReturnStringWitIdOfVideos()
        {
            _mockRepository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video>
            {
                new Video {Id = 1}, 
                new Video {Id = 2}, 
                new Video {Id = 3}
            });

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo("1,2,3"));
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_AllVideoAreProcessed_ReturnEmptyString()
        {
            _mockRepository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video>());

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            _mockFileReader.Setup(f => f.Read("video.txt")).Returns("");

            var result = _videoService.ReadVideoTitle();

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }

        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnErrorWithProperty()
        {
            _mockFileReader.Setup(f => f.Read("video.txt")).Returns("");

            var result = _videoService.ReadVideoTitle(_mockFileReader.Object);

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }
    }
}