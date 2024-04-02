using FontAwesome.Sharp;
using System.Net;
using System.Security;
using WpfApp8.Model;
using WpfApp8.Repositories;
using WpfApp8.ViewModels;
using WpfApp8.Views;

namespace StorageTests
{
	public class UnitTest1
	{
        static string password = "admin";
        SecureString securePassword = new NetworkCredential("", password).SecurePassword;
        [Fact]
        public void TestLoginViewModelWithValidCredentials()
        {
            var viewModel = new LoginViewModel();
            viewModel.Username = "admin";
            viewModel.Password = securePassword;

            viewModel.LoginCommand.Execute(null);

            Assert.Equal(false, viewModel.IsViewVisible);
            Assert.Equal(null, viewModel.ErrorMessage);
            Assert.Equal(true, Thread.CurrentPrincipal.Identity.IsAuthenticated);
            Assert.Equal("admin", Thread.CurrentPrincipal.Identity.Name);
        }

        [Fact]
        public void TestLoginViewModelWithInvalidCredentials()
        {
            var viewModel = new LoginViewModel();
            viewModel.Username = "adm";
            viewModel.Password = securePassword;
            viewModel.ErrorMessage = "* Invalid username or password";
            viewModel.LoginCommand.Execute(null);

            Assert.Equal(false, viewModel.IsViewVisible);
            Assert.Equal("* Invalid username or password", viewModel.ErrorMessage);
            Assert.Equal(true, Thread.CurrentPrincipal.Identity.IsAuthenticated);
            Assert.Equal("", Thread.CurrentPrincipal.Identity.Name);
        }


        [Fact]
        public void TestCanExecuteLoginCommandWithInvalidCredentials()
        {
            var viewModel = new LoginViewModel();
            viewModel.Username = "";
            viewModel.Password = securePassword;

            var result = viewModel.CanExecuteLoginCommand(null);

            Assert.Equal(false, result);
        }

        [Fact]
        public void GetByUsername_WhenUsernameDoesNotExist_ReturnsNull()
        {
            
            var userRepository = new UserRepository();

            
            var result = userRepository.GetByUsername("TestUser");

         
            Assert.Null(result);
        }

    }
}