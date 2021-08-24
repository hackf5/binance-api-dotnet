namespace HackF5.Binance.Api.Tests.Client
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using FakeItEasy;

    using HackF5.Binance.Api.Request.Rest;
    using HackF5.Binance.Api.Util;

    public static class TestUtilities
    {
        public static IRestClient SetupGetResponseAsync(this IRestClient rest, string relativePath)
        {
            A.CallTo(() => rest.GetResponseAsync(A<RestRequest>._, A<int>._, A<CancellationToken>._))
                .Returns(Task.FromResult(relativePath.ReadTestFileText()));
            return rest;
        }

        public static string ReadTestFileText(this string relativePath) =>
            File.ReadAllText(relativePath.GetTestPath());

        private static string GetTestPath(this string relativePath)
        {
            if (relativePath.Contains(@"\", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException(@"Use unix style paths: replace \ with /.", nameof(relativePath));
            }

            var locationUrl = new Uri(Assembly.GetExecutingAssembly().Location);
            var locationPath = Uri.UnescapeDataString(locationUrl.AbsolutePath);
            var directoryName = Path.GetDirectoryName(locationPath)
                ?? throw new InvalidOperationException($"Could not get directory name from {locationPath}.");

            return Path.Combine(directoryName, ".testFiles", relativePath);
        }
    }
}