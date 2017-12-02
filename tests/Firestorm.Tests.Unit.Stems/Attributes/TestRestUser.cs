﻿namespace Firestorm.Tests.Unit.Stems.Attributes
{
    public class TestRestUser : IRestUser
    {
        public string Username { get; set; }

        public bool IsAuthenticated
        {
            get { return Username != null; }
        }

        public bool IsInRole(string role)
        {
            return role == "TestRole";
        }
    }
}