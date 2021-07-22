using System.Collections.Generic;
using Business.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using Moq;
using Xunit;

namespace Business.UnitTests
{
    public class PlaylistManagerUnitTests
    {
        private Mock<IPlaylistService> _mock;

        public PlaylistManagerUnitTests()
        {
            _mock = new Mock<IPlaylistService>();
        }

        [Fact]
        public void GetList_Should_Return_All_Valid()
        {
            _mock.Setup(x => x.GetList()).Returns(new SuccessDataResult<List<Playlist>>(SamplePlaylists()));
            var playlistService = _mock.Object;
            var expected = SamplePlaylists();
            var actual = playlistService.GetList();
            Assert.True(actual!=null);
            Assert.Equal(expected.Count,actual.Data.Count);
        }

        [Fact]
        public void GetList_Should_Return_SuccessDataResult()
        {
            _mock.Setup(x => x.GetList()).Returns(new SuccessDataResult<List<Playlist>>(SamplePlaylists()));
            var playlistService = _mock.Object;
            var expected = new SuccessDataResult<List<Playlist>>(SamplePlaylists());
            var actual = playlistService.GetList();
            Assert.True(actual != null);
            Assert.Equal(expected.GetType(), actual.GetType());
        }

        [Fact]
        public void Get_Should_Return_Valid_Playlist()
        {
            var playlists = SamplePlaylists();
            var playlistDto = playlists[0];
            _mock.Setup(x => x.Get(It.IsAny<int>())).Returns(new SuccessDataResult<Playlist>(playlistDto));
            var playlistService = _mock.Object;
            var expected = playlistDto;
            var actual = playlistService.Get(It.IsAny<int>());
            Assert.True(actual!=null);
            Assert.Equal(expected.Id,actual.Data.Id);
            Assert.Equal(expected.VideoList,actual.Data.VideoList);
        }
        private List<Playlist> SamplePlaylists()
        {
            List<Playlist> playlists = new List<Playlist>
            {
                new Playlist
                {
                    Id = 1,
                    VideoList = "1,2,3"
                },
                new Playlist
                {
                    Id = 2,
                    VideoList = "1,2,3"
                },
                new Playlist
                {
                    Id = 3,
                    VideoList = "1,2,3"
                },
                new Playlist
                {
                    Id =4,
                    VideoList = "1,2,3"
                },
            };
            return playlists;
        }
    }
}