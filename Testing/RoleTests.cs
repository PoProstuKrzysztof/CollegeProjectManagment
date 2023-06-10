using NUnit.Framework;
using CollegeProjectManagment.Core.Domain.Entities;
using CollegeProjectManagment.Core.Interfaces;
using CollegeProjectManagment.Controllers;
using CollegeProjectManagment.Core.DTO;
using Microsoft.AspNetCore.Mvc;
using Moq;
using global::CollegeProjectManagment.Core.Mapper;
namespace CollegeProjectManagment.Tests.Domain.Entities
{
   

    namespace CollegeProjectManagment.Tests
    {
        [TestFixture]
        public class RoleControllerTests
        {
            private RoleController _roleController;
            private Mock<IRepositoryWrapper> _repositoryWrapperMock;
            private Mock<IRoleRepository> _roleRepositoryMock;
            private Mapper _mapper;

            [SetUp]
            public void Setup()
            {
                _repositoryWrapperMock = new Mock<IRepositoryWrapper>();
                _roleRepositoryMock = new Mock<IRoleRepository>();
                _mapper = new Mapper();

                _repositoryWrapperMock.Setup(rw => rw.Role).Returns(_roleRepositoryMock.Object);

                _roleController = new RoleController(_repositoryWrapperMock.Object);
            }

            [Test]
            public async Task GetAllRoles_ReturnsOkResult()
            {
                
                var roles = new List<Role>
            {
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "User" }
            };

                _roleRepositoryMock.Setup(r => r.GetAllRoles()).ReturnsAsync(roles);

                
                var result = await _roleController.GetAllRoles();

                
                Assert.IsInstanceOf<OkObjectResult>(result);
            }

            [Test]
            public async Task GetAllRoles_ReturnsNotFoundResult_WhenNoRolesExist()
            {
               
                var roles = new List<Role>();

                _roleRepositoryMock.Setup(r => r.GetAllRoles()).ReturnsAsync(roles);

                
                var result = await _roleController.GetAllRoles();

                
                Assert.IsInstanceOf<NotFoundResult>(result);
            }

            [Test]
            public async Task GetAllRoles_ReturnsInternalServerErrorResult_OnException()
            {
                
                _roleRepositoryMock.Setup(r => r.GetAllRoles()).Throws(new Exception("Test exception"));

                
                var result = await _roleController.GetAllRoles();

                
                Assert.IsInstanceOf<ObjectResult>(result);
                var objectResult = result as ObjectResult;
                Assert.AreEqual(500, objectResult.StatusCode);
                Assert.AreEqual("Test exception", objectResult.Value);
            }

            [Test]
            public async Task GetRoleById_ReturnsOkResult_WhenRoleExists()
            {
                
                var roleId = 1;
                var role = new Role { Id = roleId, Name = "Admin" };

                _roleRepositoryMock.Setup(r => r.GetRoleById(roleId)).ReturnsAsync(role);

               
                var result = await _roleController.GetRolesById(roleId);

               
                Assert.IsInstanceOf<OkObjectResult>(result);
            }

            [Test]
            public async Task GetRoleById_ReturnsNotFoundResult_WhenRoleDoesNotExist()
            {
                
                var roleId = 1;
                Role role = null;

                _roleRepositoryMock.Setup(r => r.GetRoleById(roleId)).ReturnsAsync(role);

                
                var result = await _roleController.GetRolesById(roleId);

                
                Assert.IsInstanceOf<NotFoundResult>(result);
            }

            [Test]
            public async Task GetRoleById_ReturnsInternalServerErrorResult_OnException()
            {
                
                var roleId = 1;
                _roleRepositoryMock.Setup(r => r.GetRoleById(roleId)).Throws(new Exception("Test exception"));

                
                var result = await _roleController.GetRolesById(roleId);

                
                Assert.IsTrue(result is ObjectResult);
                var objectResult = result as ObjectResult;
                Assert.AreEqual(500, objectResult.StatusCode);
                Assert.AreEqual("Test exception", objectResult.Value);
            }

            [Test]
            public void CreateRole_ReturnsCreatedResult_WhenRoleIsValid()
            {
                
                var role = new RoleDTO { Id = 1, Name = "Admin" };

                
                var result = _roleController.CreateRole(role);

               
                Assert.IsInstanceOf<CreatedAtRouteResult>(result);
            }

            [Test]
            public void CreateRole_ReturnsBadRequestResult_WhenRoleIsNull()
            {
                
                RoleDTO role = null;

                
                var result = _roleController.CreateRole(role);

                
                Assert.IsInstanceOf<BadRequestObjectResult>(result);
            }

            [Test]
            public void CreateRole_ReturnsBadRequestResult_WhenModelStateIsInvalid()
            {
                
                var role = new RoleDTO { Id = 1, Name = "" };
                _roleController.ModelState.AddModelError("Name", "The Name field is required.");

                
                var result = _roleController.CreateRole(role);

                
                Assert.IsInstanceOf<BadRequestObjectResult>(result);
            }

            [Test]
            public async Task UpdateRole_ReturnsNoContentResult_WhenRoleExists()
            {
                
                var roleId = 1;
                var role = new Role { Id = roleId, Name = "Admin" };
                var updatedRole = new RoleDTO { Id = roleId, Name = "SuperAdmin" };

                _roleRepositoryMock.Setup(r => r.GetRoleById(roleId)).ReturnsAsync(role);

                
                var result = await _roleController.UpdateRole(updatedRole);

                
                Assert.IsInstanceOf<NoContentResult>(result);
            }

            [Test]
            public async Task UpdateRole_ReturnsNotFoundResult_WhenRoleDoesNotExist()
            {
                
                var roleId = 1;
                Role role = null;
                var updatedRole = new RoleDTO { Id = roleId, Name = "SuperAdmin" };

                _roleRepositoryMock.Setup(r => r.GetRoleById(roleId)).ReturnsAsync(role);

                
                var result = await _roleController.UpdateRole(updatedRole);

               
                Assert.IsInstanceOf<NotFoundResult>(result);
            }

            [Test]
            public async Task DeleteRole_ReturnsNoContentResult_WhenRoleExists_AndNoMembersAssociated()
            {
                
                var roleId = 1;
                var role = new Role { Id = roleId, Name = "Admin" };

                _roleRepositoryMock.Setup(r => r.GetRoleById(roleId)).ReturnsAsync(role);
                _repositoryWrapperMock.Setup(rw => rw.Member.MembersByRole(roleId)).Returns(new List<Member>());

               
                var result = await _roleController.DeleteOwner(roleId);

                
                Assert.IsInstanceOf<NoContentResult>(result);
            }

            [Test]
            public async Task DeleteRole_ReturnsBadRequestResult_WhenMembersAreAssociatedWithRole()
            {
                
                var roleId = 1;
                var role = new Role { Id = roleId, Name = "Admin" };
                var members = new List<Member> { new Member() };

                _roleRepositoryMock.Setup(r => r.GetRoleById(roleId)).ReturnsAsync(role);
                _repositoryWrapperMock.Setup(rw => rw.Member.MembersByRole(roleId)).Returns(members);

                
                var result = await _roleController.DeleteOwner(roleId);

                
                Assert.IsInstanceOf<BadRequestObjectResult>(result);
            }

            [Test]
            public async Task DeleteRole_ReturnsNotFoundResult_WhenRoleDoesNotExist()
            {
               
                var roleId = 1;
                Role role = null;

                _roleRepositoryMock.Setup(r => r.GetRoleById(roleId)).ReturnsAsync(role);

               
                var result = await _roleController.DeleteOwner(roleId);

                
                Assert.IsInstanceOf<NotFoundResult>(result);
            }
        }
    }


}