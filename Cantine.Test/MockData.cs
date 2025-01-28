
using Cantine.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cantine.Test
{
    public static class MockData
    {
        public static Client GetMockStagiaireClient()
        {
            var mockClient = new Client
            {
                Id = new Guid("00000000-aaaa-bbbb-cccc-dddddddddddd"),
                Name = "Mock Name",
                Category = GetMockStagiaireCategory(),
                Budget = 20m
            };
            return mockClient;
        }
        public static Client GetMockVisiteurClient()
        {
            var mockClient = new Client
            {
                Id = new Guid("11111111-aaaa-bbbb-cccc-dddddddddddd"),
                Name = "Mock Name",
                Category = GetMockVisiteurCategory(),
                Budget = 20m
            };
            return mockClient;
        }
        public static Client GetMockVIPClient()
        {
            var mockClient = new Client
            {
                Id = new Guid("22222222-aaaa-bbbb-cccc-dddddddddddd"),
                Name = "Mock Name",
                Category = GetMockVIPCategory(),
                Budget = 20m
            };
            return mockClient;
        }
        public static Client GetMockPrestataireClient()
        {
            var mockClient = new Client
            {
                Id = new Guid("33333333-aaaa-bbbb-cccc-dddddddddddd"),
                Name = "Mock Name",
                Category = GetMockPrestataireCategory(),
                Budget = 20m
            };
            return mockClient;
        }
        public static Client GetMockInterneClient()
        {
            var mockClient = new Client
            {
                Id = new Guid("44444444-aaaa-bbbb-cccc-dddddddddddd"),
                Name = "Mock Name",
                Category = GetMockInterneCategory(),
                Budget = 20m
            };
            return mockClient;
        }

        public static ClientCategory GetMockStagiaireCategory() {
            var mockCategory = new ClientCategory
            {
                CategoryId = new Guid("aaaaaaaa-bbbb-cccc-dddd-000000000000"),
                Name = "Stagiaire",
                DiscountType = "Fixed",
                DiscountValue = 10m
            };
            return mockCategory;
        }
        public static ClientCategory GetMockVIPCategory() {
            var mockCategory = new ClientCategory
            {
                CategoryId = new Guid("aaaaaaaa-bbbb-cccc-dddd-111111111111"),
                Name = "VIP",
                DiscountType = "Percentage",
                DiscountValue = 100m
            };
            return mockCategory;
        }
        public static ClientCategory GetMockVisiteurCategory() {
            var mockCategory = new ClientCategory
            {
                CategoryId = new Guid("aaaaaaaa-bbbb-cccc-dddd-222222222222"),
                Name = "Visiteur",
                DiscountType = "None",
                DiscountValue = 0m
            };
            return mockCategory;
        }
        public static ClientCategory GetMockPrestataireCategory()
        {
            var mockCategory = new ClientCategory
            {
                CategoryId = new Guid("aaaaaaaa-bbbb-cccc-dddd-333333333333"),
                Name = "Prestataire",
                DiscountType = "Fixed",
                DiscountValue = 6m
            };
            return mockCategory;
        }
        public static ClientCategory GetMockInterneCategory()
        {
            var mockCategory = new ClientCategory
            {
                CategoryId = new Guid("aaaaaaaa-bbbb-cccc-dddd-444444444444"),
                Name = "Interne",
                DiscountType = "Fixed",
                DiscountValue = 7.5m
            };
            return mockCategory;
        }

        public static List<string> GetMockListProducts()
        {
            return new List<string> { "Pain", "Grand Salade Bar", "Portion de fruit", "Dessert" };
        }
    }
}
