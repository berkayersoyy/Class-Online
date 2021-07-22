using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Extras.Moq;
using DataAccess.Abstract;
using Entities.Concrete;
using Moq;
using Xunit;

namespace DataAccess.UnitTests
{
    public class VideoUnitTests
    {
        private Mock<IVideoDal> _mock;
        public VideoUnitTests()
        {
            _mock = new Mock<IVideoDal>();
        }
        [Fact]
        public void GetAll_Should_Return_All_Videos_Count_Valid()
        {
            _mock.Setup(x => x.GetAll(null)).Returns(SampleVideos());
            var videoDal = _mock.Object;
            var expected = SampleVideos().Count;
            var actual = videoDal.GetAll();
            Assert.True(actual!=null);
            Assert.Equal(expected,actual.Count);
        }

        [Fact]
        public void Get_Should_Return_1()
        {
            var videos = SampleVideos();
            var videoDto = videos[0];
            _mock.Setup(x => x.Get(x => x.Id==1)).Returns(videoDto);
            var videoDal = _mock.Object;
            var expected = videos.FirstOrDefault(x=>x.Id==1);
            var actual = videoDal.Get(x=>x.Id==1);
            Assert.True(actual!=null);
            Assert.Equal(expected.Id,actual.Id);
            Assert.Equal(expected.Title,actual.Title);
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
