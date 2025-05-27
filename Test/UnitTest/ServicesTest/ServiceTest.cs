using AutoMapper;
using Domain;
using Infrastructure.Repositories;
using Infrastructure.Repositories.IRepositories;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using Services.Common;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace UnitTest
{
    public class ServiceTest
    {
        Service service;
        IMapper mapper;
        Mock<IRepository> repository;
        Mock<UserManager<ApplicationUser>> userManagerMock;
        List<Milk> returnList;

        [SetUp]
        public void Setup()
        {
            var myProfile = new MProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            mapper = new Mapper(configuration);

            repository = new Mock<IRepository>();
            returnList = new List<Milk>();
            returnList.Add(new Milk() { Id = Guid.NewGuid(), Liters = 10, RecolectionDate = DateTime.Now });
            repository.Setup(x => x.GetAllMilks()).ReturnsAsync(returnList);

            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            userManagerMock = new Mock<UserManager<ApplicationUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());
            service = new Service(repository.Object, mapper, dbContext, userManagerMock.Object);
        }

        [Test]
        public void Service_Exist()
        {
            Assert.That(service, Is.Not.Null);
        }

        [Test]
        public async Task Service_CanGetMilkList()
        {
            var result = await service.GetAllMilks();
            Assert.That(result.Count, Is.EqualTo(1));
        }
    }
}
