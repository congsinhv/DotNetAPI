using DotnetAPIProject.Models.DTOs;
using DotnetAPIProject.Models.Entities;

namespace DotnetAPIProject.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<Account>> GetAccountsAsync();
        Task<Account> AddAccountAsync(AccountDto account);
        //Task<Workspace?> UpdateWorkspaceAsync(int id, WorkspaceDto workspace);
    }
}
