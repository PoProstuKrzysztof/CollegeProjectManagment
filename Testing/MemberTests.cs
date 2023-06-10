using CollegeProjectManagment.Controllers;
using CollegeProjectManagment.Core.Domain.Entities;
using CollegeProjectManagment.Core.DTO;
using CollegeProjectManagment.Core.Interfaces;
using CollegeProjectManagment.Core.Mapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollegeProjectManagment.Tests.Controllers
{
    [TestFixture]
    public class MemberControllerTests
    {
        private Mock<IRepositoryWrapper> _repositoryWrapperMock;
        private Mock<IMemberRepository> _memberRepositoryMock;
        private MemberController _memberController;
        private Mapper _mapper;

        [SetUp]
        public void Setup()
        {
            _repositoryWrapperMock = new Mock<IRepositoryWrapper>();
            _memberRepositoryMock = new Mock<IMemberRepository>();
            _repositoryWrapperMock.Setup(rw => rw.Member).Returns(_memberRepositoryMock.Object);
            _memberController = new MemberController(_repositoryWrapperMock.Object);
            _mapper = new Mapper();
        }

        [Test]
        public async Task GetAllMembers_ReturnsOkResult_WhenMembersExist()
        {
            // Arrange
            var members = new List<Member>
            {
                new Member { Id = 1, Name = "John", Surname = "Doe" },
                new Member { Id = 2, Name = "Jane", Surname = "Smith" }
            };
            _memberRepositoryMock.Setup(m => m.GetAllMembers()).ReturnsAsync(members);

            // Act
            var result = await _memberController.GetAllMembers();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            var memberDTOs = okResult.Value as List<MemberDTO>;
            Assert.IsNotNull(memberDTOs);
            Assert.AreEqual(members.Count, memberDTOs.Count);
            Assert.AreEqual(members[0].Id, memberDTOs[0].Id);
            Assert.AreEqual(members[0].Name, memberDTOs[0].Name);
            Assert.AreEqual(members[0].Surname, memberDTOs[0].Surname);
            Assert.AreEqual(members[1].Id, memberDTOs[1].Id);
            Assert.AreEqual(members[1].Name, memberDTOs[1].Name);
            Assert.AreEqual(members[1].Surname, memberDTOs[1].Surname);
        }

        [Test]
        public async Task GetAllMembers_ReturnsNotFoundResult_WhenNoMembersExist()
        {
            // Arrange
            var members = new List<Member>();
            _memberRepositoryMock.Setup(m => m.GetAllMembers()).ReturnsAsync(members);

            // Act
            var result = await _memberController.GetAllMembers();

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task GetMemberById_ReturnsOkResult_WhenMemberExists()
        {
            // Arrange
            var memberId = 1;
            var member = new Member { Id = memberId, Name = "John", Surname = "Doe" };
            _memberRepositoryMock.Setup(m => m.GetMemberById(memberId)).ReturnsAsync(member);

            // Act
            var result = await _memberController.GetMemberById(memberId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            var memberDTO = okResult.Value as MemberDTO;
            Assert.IsNotNull(memberDTO);
            Assert.AreEqual(member.Id, memberDTO.Id);
            Assert.AreEqual(member.Name, memberDTO.Name);
            Assert.AreEqual(member.Surname, memberDTO.Surname);
        }

        [Test]
        public async Task GetMemberById_ReturnsNotFoundResult_WhenMemberDoesNotExist()
        {
            // Arrange
            var memberId = 1;
            _memberRepositoryMock.Setup(m => m.GetMemberById(memberId)).ReturnsAsync((Member)null);

            // Act
            var result = await _memberController.GetMemberById(memberId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task CreateMember_ReturnsCreatedAtRouteResult_WhenMemberIsCreated()
        {
            // Arrange
            var memberDTO = new MemberDTO { Id = 1, Name = "John", Surname = "Doe" };
            var member = _mapper.MapMemberDtoToMember(memberDTO);
            _memberRepositoryMock.Setup(m => m.CreateMember(member));

            // Act
            var result = await _memberController.CreateMember(memberDTO);

            // Assert
            Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            var createdAtRouteResult = result as CreatedAtRouteResult;
            Assert.AreEqual("MemberById", createdAtRouteResult.RouteName);
            Assert.AreEqual(member.Id, createdAtRouteResult.RouteValues["id"]);
        }

        [Test]
        public async Task CreateMember_ReturnsBadRequestResult_WhenMemberObjectIsNull()
        {
            // Arrange
            MemberDTO memberDTO = null;

            // Act
            var result = await _memberController.CreateMember(memberDTO);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task CreateMember_ReturnsBadRequestResult_WhenModelStateIsInvalid()
        {
            // Arrange
            var memberDTO = new MemberDTO { Id = 1, Name = "", Surname = "Doe" };
            _memberController.ModelState.AddModelError("Name", "Name is required.");

            // Act
            var result = await _memberController.CreateMember(memberDTO);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task UpdateMember_ReturnsNoContentResult_WhenMemberIsUpdated()
        {
            // Arrange
            var memberDTO = new MemberDTO { Id = 1, Name = "John", Surname = "Doe" };
            var existingMember = new Member { Id = 1, Name = "OldName", Surname = "OldSurname" };
            _memberRepositoryMock.Setup(m => m.GetMemberById(memberDTO.Id)).ReturnsAsync(existingMember);
            _memberRepositoryMock.Setup(m => m.UpdateMember(existingMember));

            // Act
            var result = await _memberController.UpdateMember(memberDTO);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task UpdateMember_ReturnsNotFoundResult_WhenMemberDoesNotExist()
        {
            // Arrange
            var memberDTO = new MemberDTO { Id = 1, Name = "John", Surname = "Doe" };
            _memberRepositoryMock.Setup(m => m.GetMemberById(memberDTO.Id)).ReturnsAsync((Member)null);

            // Act
            var result = await _memberController.UpdateMember(memberDTO);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task DeleteMember_ReturnsNoContentResult_WhenMemberIsDeleted()
        {
            // Arrange
            var memberId = 1;
            var existingMember = new Member { Id = memberId, Name = "John", Surname = "Doe" };
            _memberRepositoryMock.Setup(m => m.GetMemberById(memberId)).ReturnsAsync(existingMember);
            _memberRepositoryMock.Setup(m => m.DeleteMember(existingMember));
            _repositoryWrapperMock.Setup(rw => rw.Save());

            // Act
            var result = await _memberController.DeleteMember(memberId);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task DeleteMember_ReturnsNotFoundResult_WhenMemberDoesNotExist()
        {
            // Arrange
            var memberId = 1;
            _memberRepositoryMock.Setup(m => m.GetMemberById(memberId)).ReturnsAsync((Member)null);

            // Act
            var result = await _memberController.DeleteMember(memberId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}
