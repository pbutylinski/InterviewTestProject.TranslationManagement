using AutoFixture;
using AutoMapper;
using Moq;
using TranslationManagement.DataAccess;
using TranslationManagement.DataAccess.Models;
using TranslationManagement.Domain.Services;

namespace TranslationManagement.Domain.Tests.Services
{
    public class TranslationJobServiceTests
    {
        private readonly TranslationJobService sut;
        private readonly Fixture fixture;

        private readonly Mock<IRepository<TranslationJob>> repositoryMock;
        private readonly Mock<IPriceCalculationService> priceCalculationMock;
        private readonly Mock<IUnreliableServiceWrapper> serviceWrapperMock;
        private readonly Mock<IMapper> mapperMock;

        public TranslationJobServiceTests()
        {
            this.repositoryMock = new Mock<IRepository<TranslationJob>>();
            this.priceCalculationMock = new Mock<IPriceCalculationService>();
            this.serviceWrapperMock = new Mock<IUnreliableServiceWrapper>();
            this.mapperMock = new Mock<IMapper>();

            this.sut = new TranslationJobService(
                this.repositoryMock.Object,
                this.priceCalculationMock.Object,
                this.serviceWrapperMock.Object,
                this.mapperMock.Object);

            this.fixture = new Fixture();
        }

        [SetUp]
        public void Setup()
        {
            this.serviceWrapperMock.Reset();
        }

        [Test]
        public async Task When_JobIsCreatedSuccessfully_Then_NotificationIsSent()
        {
            // Setup
            var job1 = this.fixture.Create<Models.TranslationJob>();
            var job2 = this.fixture.Create<TranslationJob>();
            this.mapperMock.Setup(x => x.Map<TranslationJob>(It.IsAny<Models.TranslationJob>())).Returns(job2);
            this.repositoryMock.Setup(x => x.Create(It.IsAny<TranslationJob>())).ReturnsAsync(1);

            // Act
            await this.sut.Create(job1);

            // Assert
            this.serviceWrapperMock.Verify(x => x.TrySendNewJobNotification(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public async Task When_JobIsNotCreatedSuccessfully_Then_NotificationWasNotSent()
        {
            // Setup
            var job1 = this.fixture.Create<Models.TranslationJob>();
            var job2 = this.fixture.Create<TranslationJob>();
            this.mapperMock.Setup(x => x.Map<TranslationJob>(It.IsAny<Models.TranslationJob>())).Returns(job2);
            this.repositoryMock.Setup(x => x.Create(It.IsAny<TranslationJob>())).ReturnsAsync(0);

            // Act
            await this.sut.Create(job1);

            // Assert
            this.serviceWrapperMock.Verify(x => x.TrySendNewJobNotification(It.IsAny<int>()), Times.Never);
        }
    }
}
