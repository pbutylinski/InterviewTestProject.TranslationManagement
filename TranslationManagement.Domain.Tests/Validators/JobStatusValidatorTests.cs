using TranslationManagement.Domain.Validators;
using static TranslationManagement.Common.Constants.JobStatus;

namespace TranslationManagement.Domain.Tests.Validators
{
    // Most tests here can be done using parametrized test cases
    public class JobStatusValidatorTests
    {
        private readonly IJobStatusValidator sut;

        public JobStatusValidatorTests()
        {
            this.sut = new JobStatusValidator();
        }

        [Test]
        public void When_StatusMovesFromNewToInProgress_Then_ValidationPasses()
        {
            Assert.DoesNotThrow(() => this.sut.ValidateAndThrow(New, InProgress));
        }

        [Test]
        public void When_StatusMovesInProgressToCompleted_Then_ValidationPasses()
        {
            Assert.DoesNotThrow(() => this.sut.ValidateAndThrow(InProgress, Completed));
        }

        [Test]
        public void When_StatusesAreNull_Then_ValidationFails()
        {
            Assert.Throws(typeof(ValidationException), 
                () => this.sut.ValidateAndThrow(null, null));
        }

        [Test]
        public void When_StatusesAreEmpty_Then_ValidationFails()
        {
            Assert.Throws(typeof(ValidationException), 
                () => this.sut.ValidateAndThrow(string.Empty, string.Empty));
        }

        [Test]
        public void When_StatusesAreInvalid_Then_ValidationFails()
        {
            Assert.Throws(typeof(ValidationException),
                () => this.sut.ValidateAndThrow("qwerty", "test123"));
        }

        [Test]
        public void When_StatusMovesFromNewToCompleted_Then_ValidationFails()
        {
            Assert.Throws(typeof(ValidationException),
                () => this.sut.ValidateAndThrow(New, Completed));
        }

        [Test]
        public void When_StatusMovesFromCompletedToNew_Then_ValidationFails()
        {
            Assert.Throws(typeof(ValidationException),
                () => this.sut.ValidateAndThrow(Completed, New));
        }

        [Test]
        public void When_StatusMovesFromInProgressToNew_Then_ValidationFails()
        {
            Assert.Throws(typeof(ValidationException),
                () => this.sut.ValidateAndThrow(InProgress, New));
        }
    }
}
