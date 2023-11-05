using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace CleanA.Domain.Entitys.User;

public class WalletUser
{
    public int Id { get; set; }
    [ForeignKey(nameof(IdentityUser))]
    public Guid UserId { get; set; }
    public int Balance { get; set; }
}