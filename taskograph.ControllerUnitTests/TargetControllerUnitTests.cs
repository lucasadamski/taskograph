﻿using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using taskograph.EF.Repositories;
using taskograph.EF.Repositories.Infrastructure;
using taskograph.Models;
using taskograph.Web.Controllers;
using taskograph.Web.Models;
using taskograph.Web.Models.DTOs;

namespace taskograph.ControllerUnitTests
{
    public class TargetControllerUnitTests
    {
        private IPreciseTargetRepository _preciseTargetRepository;
        private IRegularTargetRepository _regularTargetRepository;

        private readonly ILogger<TargetController> _logger;
        private IConfiguration _configuration;

        private TargetController _targetController;

        string _userId = "test user";

        public TargetControllerUnitTests()
        {
            // Dependencies;
            _preciseTargetRepository = A.Fake<IPreciseTargetRepository>();
            _regularTargetRepository = A.Fake<IRegularTargetRepository>();
            _logger = A.Fake<ILogger<TargetController>>();
            _configuration = A.Fake<IConfiguration>();

            // Subject Under Testing
            _targetController = new TargetController(_preciseTargetRepository, _regularTargetRepository, _logger, _configuration);
        }
    }
}
