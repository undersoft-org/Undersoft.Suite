using Microsoft.VisualStudio.TestTools.UnitTesting;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Series;
using Undersoft.SDK.Uniques;
using Undersoft.SDK.Updating;

namespace Undersoft.SDK.Tests.Instant.Updating
{
    [TestClass]
    public class UpdaterTest
    {
        [TestMethod]
        public void updater_Integrated_Test()
        {
            var profile = new Agreement()
            {
                Comments = "fdfdsgg",
                Created = DateTime.Now,
                Kind = AgreementKind.Agree,
                Creator = "sfssd",
                VersionId = 222,
                Formats = new Listing<AgreementFormat>()
                {
                    new AgreementFormat() { Id = 10, Name = "telefon" },
                    new AgreementFormat() { Id = 15, Name = "skan" }
                },
                Versions = new Listing<AgreementVersion>()
                {
                    new AgreementVersion()
                    {
                        Id = 20,
                        VersionNumber = 2,
                        Texts = new Listing<AgreementText>()
                        {
                            new AgreementText()
                            {
                                VersionId = 2,
                                Language = "en",
                                Content = "dfsdgdsdfsgfd"
                            },
                            new AgreementText()
                            {
                                VersionId = 2,
                                Language = "pl",
                                Content = "telefon"
                            }
                        }
                    },
                    new AgreementVersion()
                    {
                        Id = 25,
                        VersionNumber = 5,
                        Texts = new Listing<AgreementText>()
                        {
                            new AgreementText()
                            {
                                VersionId = 5,
                                Language = "en",
                                Content = "dfsdgdsdfsgfd"
                            },
                            new AgreementText()
                            {
                                VersionId = 5,
                                Language = "pl",
                                Content = "telefon"
                            }
                        }
                    }
                }
            };

            var updater6 = new Updater<Agreement>(profile);

            var Proxy = new ProxyCreator<Agreement>();
            var updater0 = new Updater<UserProfile>();
            var updater1 = new Updater<User>();

            var user = new User()
            {
                Email = "jflskdfjlkdj",
                FirstName = "hfdjkfsjdkh",
                LastName = "fdlfhjdsk",
                OperationDate = DateTime.Now
            };

            user.Sign(user);

            var updater3 = new Updater<User>(user);

            var userprofile = new UserProfile()
            {
                Email = "43423423",
                Name = "8978787",
                Surname = "43432432",
                Created = DateTime.Now
            };

            userprofile.Sign(userprofile);

            var updater5 = new Updater<UserProfile>(userprofile);

            var dataObject0 = userprofile;

            var _Proxy0 = profile.ToProxy();
            var _Proxy1 = _Proxy0.ToProxy();

            var mock = new Agreement()
            {
                Comments = "fdsfsf",
                Created = DateTime.Now,
                Kind = AgreementKind.Cancellation,
                Creator = "fsds",
                VersionId = 992
            };


            mock.PatchTo(updater3.Source);
            dataObject0.PatchFrom(updater0.Source);
            dataObject0.PutTo(updater1.Source);
            dataObject0.PutFrom(mock);

            ((Agreement)_Proxy0).TypeId = 1005L;
            var uk = _Proxy0.Id;
            var k2 = _Proxy0["Id"];
            _Proxy0.Id = 2500L;

            _Proxy0["Id"] = 1005L;
            _Proxy0["TypeId"] = 1005L;
            object o = _Proxy1[5];

            profile.Time = DateTime.Now;

            var serial = new Uscn(profile.CodeNo);
        }
    }
}
