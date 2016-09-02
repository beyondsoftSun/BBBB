
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GitTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var credentials = new VssAadCredential();
            var messageHandler = new VssHttpMessageHandler(credentials, new VssHttpRequestSettings());
            Uri uri = new Uri(@"https://microsoft.visualstudio.com/");
            GitHttpClient gitHttpClient = new GitHttpClient(uri, messageHandler.Credentials);
            var Repositories = gitHttpClient.GetRepositoriesAsync().Result;
            GitRepository repository = Repositories.FirstOrDefault(r => r.Name.ToLowerInvariant() == "Localization".ToLowerInvariant());
            var gitBranchStatuss = gitHttpClient.GetBranchesAsync(repository.Id).Result;
            GitBranchStats gitBranchStatus = gitBranchStatuss.FirstOrDefault(branch => branch.Name.ToLowerInvariant() == "master");


            var descriptor = new GitVersionDescriptor() { Version = gitBranchStatus.Name, VersionOptions = GitVersionOptions.None, VersionType = GitVersionType.Branch };

            //GitItem item = gitHttpClient.GetItemAsync(repositoryId: repository.Id, path: "/intl/af-za/loc/windows/lcl/aad/brokerplugin/microsoft.aad.brokerplugin.dll.lcl", scopePath: "/intl/af-za/loc/windows/lcl/aad/brokerplugin/microsoft.aad.brokerplugin.dll.lcl", recursionLevel: VersionControlRecursionType.OneLevel, includeContentMetadata: true, latestProcessedChange: true, download: true, versionDescriptor: descriptor, userState: null, cancellationToken: new CancellationToken()).Result;


            VersionControlProjectInfo vvvvvv= new VersionControlProjectInfo();



            List<GitItem> items = gitHttpClient.GetItemsAsync(repositoryId: repository.Id, scopePath: "/intl/af-za/loc/windows/lcl/aad/brokerplugin/microsoft.aad.brokerplugin.dll.lcl", recursionLevel: VersionControlRecursionType.OneLevel, includeContentMetadata: true, latestProcessedChange: true, download: true, includeLinks: false, versionDescriptor: descriptor, userState: null, cancellationToken: new CancellationToken()).Result;


            List<GitCommitRef> aaaa = gitHttpClient.GetCommitsAsync(repositoryId: repository.Id, searchCriteria: new GitQueryCommitsCriteria(), skip: null, top: null, userState: null, cancellationToken: new CancellationToken()).Result;

            GitCommitChanges gitCommitChanges = gitHttpClient.GetChangesAsync(items[0].CommitId, repositoryId: repository.Id, top: null, skip: null, userState: null, cancellationToken: new CancellationToken()).Result;











            Stream ssss = gitHttpClient.GetItemContentAsync(repositoryId: repository.Id,path: items[0].Path, recursionLevel: VersionControlRecursionType.None, includeContentMetadata: true, latestProcessedChange: true, download: true, versionDescriptor: descriptor, userState: null, cancellationToken: new CancellationToken()).Result;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                ssss.CopyTo(memoryStream);

                // Use StreamReader to read MemoryStream created from byte array
                using (StreamReader streamReader = new StreamReader(new MemoryStream(memoryStream.ToArray())))
                {
                    string fileString = streamReader.ReadToEnd();
                }
            }
        }
    }
}
