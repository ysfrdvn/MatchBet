using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MatchBet.Player.Data.Mappings;

public class PlayerMapping
{
    public PlayerMapping(EntityTypeBuilder<Models.Player> entityTypeBuilder)
    {
        entityTypeBuilder.HasKey(q => q.Id);
        entityTypeBuilder.ToTable("AspNetUsers");

        entityTypeBuilder.Property(q => q.Id).HasColumnName("Id");
        entityTypeBuilder.Property(q => q.UserName).HasColumnName("UserName");
        entityTypeBuilder.Property(q => q.Email).HasColumnName("Email");
        entityTypeBuilder.Property(q => q.Credit).HasColumnName("Credit");
        entityTypeBuilder.Property(q => q.Score).HasColumnName("Score");
    }
}