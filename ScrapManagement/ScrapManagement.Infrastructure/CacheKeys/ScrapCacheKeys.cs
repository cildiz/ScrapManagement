namespace ScrapManagement.Infrastructure.CacheKeys
{
    public static class ScrapCacheKeys
    {
        public static string ListKey => "ScrapList";

        public static string SelectListKey => "ScrapSelectList";

        public static string GetKey(int scrapId) => $"Scrap-{scrapId}";

        public static string GetDetailsKey(int scrapId) => $"ScrapDetails-{scrapId}";
    }
}