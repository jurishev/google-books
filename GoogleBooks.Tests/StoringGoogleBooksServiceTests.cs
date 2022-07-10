using GoogleBooks.Models;
using GoogleBooks.Services;
using GoogleBooks.Storage;
using Moq;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace GoogleBooks.Tests;

public class StoringGoogleBooksServiceTests
{
    private const string GoogleApiMsg = "Google API should have been called.";
    private const string NoGoogleApiMsg = "Google API should NOT have been called.";

    private static readonly Random Rand = new();

    private readonly string volumeId;
    private readonly Mock<IGoogleBooksStorage> storageMock;

    public StoringGoogleBooksServiceTests()
    {
        this.volumeId = Guid.NewGuid().ToString();
        this.storageMock = new Mock<IGoogleBooksStorage>();
    }

    [Fact]
    public async Task GetVolume_Having_EntityInStorage_ShouldNot_CallGoogleApi()
    {
        // Arrange

        var httpHandlerMock = new HttpMessageHandlerMock();
        var service = new StoringGoogleBooksService(this.storageMock.Object, new HttpClient(httpHandlerMock));
        var volume = new Volume { Id = this.volumeId };

        this.storageMock.Setup(storage => storage.GetVolume(this.volumeId)).Returns(volume);

        // Act

        var result = await service.GetVolume(this.volumeId);

        // Assert

        this.storageMock.Verify(storage => storage.GetVolume(this.volumeId), Times.Once);

        Assert.False(httpHandlerMock.RequestPerformed, NoGoogleApiMsg);
        Assert.NotNull(result);
        Assert.Same(volume, result);
    }

    [Fact]
    public async Task GetVolume_Having_NoEntityInStorage_Should_CallGoogleApi()
    {
        // Arrange

        var url = $"https://www.googleapis.com/books/v1/volumes/{this.volumeId}";
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = JsonContent.Create(new Volume { Id = this.volumeId })
        };

        var httpHandlerMock = new HttpMessageHandlerMock(url, response);
        var service = new StoringGoogleBooksService(this.storageMock.Object, new HttpClient(httpHandlerMock));

        this.storageMock.Setup(storage => storage.GetVolume(It.IsAny<string>())).Returns(null as Volume);

        // Act

        var result = await service.GetVolume(this.volumeId);

        // Assert

        this.storageMock.Verify(storage => storage.GetVolume(this.volumeId), Times.Once);

        Assert.True(httpHandlerMock.RequestPerformed, GoogleApiMsg);
        Assert.NotNull(result);
        Assert.Equal(this.volumeId, result.Id);
    }

    [Fact]
    public async Task GetVolume_Having_NoEntity_Should_ReturnNull()
    {
        // Arrange

        var httpHandlerMock = new HttpMessageHandlerMock();
        var service = new StoringGoogleBooksService(this.storageMock.Object, new HttpClient(httpHandlerMock));

        this.storageMock.Setup(storage => storage.GetVolume(It.IsAny<string>())).Returns(null as Volume);

        // Act

        var result = await service.GetVolume(this.volumeId);

        // Accert

        this.storageMock.Verify(storage => storage.GetVolume(this.volumeId), Times.Once);

        Assert.True(httpHandlerMock.RequestPerformed, GoogleApiMsg);
        Assert.Null(result);
    }

    [Theory]
    [MemberData(nameof(GetQueries))]
    public async Task GetVolumeList_Having_EntitiesInStorage_ShouldNot_CallGoogleApi(VolumeQuery query)
    {
        // Arrange

        var httpHandlerMock = new HttpMessageHandlerMock();
        var service = new StoringGoogleBooksService(this.storageMock.Object, new HttpClient(httpHandlerMock));
        var volumes = GetVolumes(Rand.Next(2, 5), query).ToList();

        this.storageMock.Setup(storage => storage.GetVolumeList(query)).Returns(volumes);

        // Act

        var result = await service.GetVolumeList(query);

        // Assert

        this.storageMock.Verify(storage => storage.GetVolumeList(query), Times.Once);

        Assert.False(httpHandlerMock.RequestPerformed, NoGoogleApiMsg);
        Assert.NotNull(result);
        Assert.Same(volumes, result);
    }

    [Theory]
    [MemberData(nameof(GetQueries))]
    public async Task GetVolumeList_Having_NoEntitiesInStorage_Should_CallGoogleApi(VolumeQuery query)
    {
        // Arrange

        var volumes = GetVolumes(Rand.Next(2, 5), query).ToList();
        var url = $"https://www.googleapis.com/books/v1/volumes?q={GetTerms(query)}";
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = JsonContent.Create(new VolumeList { Items = volumes, TotalItems = volumes.Count })
        };

        var httpHandlerMock = new HttpMessageHandlerMock(url, response);
        var service = new StoringGoogleBooksService(this.storageMock.Object, new HttpClient(httpHandlerMock));

        this.storageMock.Setup(storage => storage.GetVolumeList(It.IsAny<VolumeQuery>())).Returns(null as IEnumerable<Volume>);

        // Act

        var result = await service.GetVolumeList(query);

        // Assert

        this.storageMock.Verify(storage => storage.GetVolumeList(query), Times.Once);

        Assert.True(httpHandlerMock.RequestPerformed, GoogleApiMsg);
        Assert.NotNull(result);
        Assert.Equal(JsonSerializer.Serialize(volumes), JsonSerializer.Serialize(result));
    }

    [Theory]
    [MemberData(nameof(GetQueries))]
    public async Task GetVolumeList_Having_NoEntities_Should_ReturnNull(VolumeQuery query)
    {
        // Arrange

        var httpHandlerMock = new HttpMessageHandlerMock();
        var service = new StoringGoogleBooksService(this.storageMock.Object, new HttpClient(httpHandlerMock));

        this.storageMock.Setup(storage => storage.GetVolumeList(query)).Returns(null as IEnumerable<Volume>);

        // Act

        var result = await service.GetVolumeList(query);

        // Accert

        this.storageMock.Verify(storage => storage.GetVolumeList(query), Times.Once);

        Assert.True(httpHandlerMock.RequestPerformed, GoogleApiMsg);
        Assert.Null(result);
    }

    private static IEnumerable<object[]> GetQueries()
    {
        yield return new object[] { new VolumeQuery { Title = Guid.NewGuid().ToString() } };
        yield return new object[] { new VolumeQuery { Author = Guid.NewGuid().ToString() } };
        yield return new object[] { new VolumeQuery { Subject = Guid.NewGuid().ToString() } };
        yield return new object[] { new VolumeQuery { Publisher = Guid.NewGuid().ToString() } };
        yield return new object[] { new VolumeQuery { Isbn = Guid.NewGuid().ToString() } };

        yield return new object[]
        {
            new VolumeQuery
            {
                Title = Guid.NewGuid().ToString(),
                Author = Guid.NewGuid().ToString(),
            }
        };

        yield return new object[]
        {
            new VolumeQuery
            {
                Title = Guid.NewGuid().ToString(),
                Author = Guid.NewGuid().ToString(),
                Subject = Guid.NewGuid().ToString(),
                Publisher = Guid.NewGuid().ToString(),
                Isbn = Guid.NewGuid().ToString(),
            }
        };
    }

    private static IEnumerable<Volume> GetVolumes(int count, VolumeQuery query)
    {
        while (count-- > 0)
        {
            yield return new Volume
            {
                Id = Guid.NewGuid().ToString(),
                VolumeInfo = new VolumeInfo
                {
                    Title = query.Title,
                    Authors = new string[] { query.Author },
                    Categories = new string[] { query.Subject },
                    Publisher = query.Publisher,
                    IndustryIdentifiers = new IndustryIdentifier[]
                    {
                        new IndustryIdentifier { Type = "ISBN", Identifier = query.Isbn }
                    }
                }
            };
        }
    }

    private static string GetTerms(VolumeQuery query)
    {
        var terms = new List<string>();

        AddTerm("intitle", query.Title, terms);
        AddTerm("inauthor", query.Author, terms);
        AddTerm("inpublisher", query.Publisher, terms);
        AddTerm("subject", query.Subject, terms);
        AddTerm("isbn", query.Isbn, terms);

        return string.Join('+', terms);
    }

    private static void AddTerm(string key, string value, List<string> terms)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            terms.Add($"{key}:{value}");
        }
    }
}
