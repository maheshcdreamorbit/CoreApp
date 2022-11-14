﻿using System;
using System.Net.Http;
using Core.Data.Context;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore.Storage;

namespace Core.IntegrationTest.Fixtures
{
    public class WebFixture<TStartup> : Fixture, IDisposable where TStartup : class
    {
        private readonly IServiceProvider _services;

        public HttpClient Client;

        public DatabaseContext DbContext { get; }

        public IDbContextTransaction Transaction;

        public WebFixture()
        {
            IWebHostBuilder builder = WebHost.CreateDefaultBuilder()
                .UseStartup<TStartup>();
			var server = new TestServer(builder);
			Client = server.CreateClient();
			_services = server.Host.Services;

			DbContext = GetService<DatabaseContext>();
            Transaction = DbContext.Database.BeginTransaction();
        }

        protected T GetService<T>() => (T)_services.GetService(typeof(T));

        public void Dispose()
        {
            if (Transaction == null) return;

            Transaction.Rollback();
            Transaction.Dispose();
        }
    }
}