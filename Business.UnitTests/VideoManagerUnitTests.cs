using System;
using System.Collections.Generic;
using Business.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using Moq;
using Xunit;

namespace Business.UnitTests
{
    public class VideoManagerUnitTests
    {
        private Mock<IVideoService> _mock;

        public VideoManagerUnitTests()
        {
            _mock = new Mock<IVideoService>();
        }
        [Fact]
        public void GetList_Should_Return_All()
        {
            _mock.Setup(x => x.GetList()).Returns(new SuccessDataResult<List<Video>>(SampleVideos()));
            var videoService = _mock.Object;
            var expected = SampleVideos().Count;
            var actual = videoService.GetList().Data;
            Assert.True(actual!=null);
            Assert.Equal(expected,actual.Count);
        }

        [Fact]
        public void GetList_Should_Return_SuccessDataResult()
        {
            _mock.Setup(x => x.GetList()).Returns(new SuccessDataResult<List<Video>>(SampleVideos()));
            var videoService = _mock.Object;
            var videos = new SuccessDataResult<List<Video>>(SampleVideos());
            var expected = videos.GetType();
            var actual = videoService.GetList().GetType();
            Assert.True(actual != null);
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void GetList_Should_Return_ErrorDataResult()
        {
            _mock.Setup(x => x.GetList()).Returns(new ErrorDataResult<List<Video>>(SampleVideos()));
            var videoService = _mock.Object;
            var videos = new ErrorDataResult<List<Video>>(SampleVideos());
            var expected = videos.GetType();
            var actual = videoService.GetList().GetType();
            Assert.True(actual != null);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Get_Should_Return_Single_Video()
        {
            var videos = SampleVideos();
            var videoDto = new SuccessDataResult<Video>(videos[0]);
            _mock.Setup(x => x.Get(1)).Returns(videoDto);
            var videoService = _mock.Object;
            var expected = new SuccessDataResult<Video>(videos[0]);
            var actual = videoService.Get(1);
            Assert.True(actual!=null);
            Assert.Equal(expected.GetType(),actual.GetType());
            Assert.Equal(expected.Data.Id,actual.Data.Id);
        }

        private List<Video> SampleVideos()
        {
            List<Video> videos = new List<Video>
            {
                new Video
                {
                    Extension = "mp4",
                    Title = "Video 1",
                    Description = "video",
                    Path = "videos",
                    Id = 1
                },
                new Video
                {
                    Extension = "mp4",
                    Title = "Video 2",
                    Description = "video",
                    Path = "videos",
                    Id = 2
                },
                new Video
                {
                    Extension = "mp4",
                    Title = "Video 3",
                    Description = "video",
                    Path = "videos",
                    Id = 3
                },

            };
            return videos;
        }
    }
}
