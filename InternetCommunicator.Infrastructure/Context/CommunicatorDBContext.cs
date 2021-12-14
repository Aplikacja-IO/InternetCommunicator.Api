using InternetCommunicator.Domain.Models;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace InternetCommunicator.Infrastructure.Context
{
    public partial class CommunicatorDbContext : DbContext
    {
        public CommunicatorDbContext(DbContextOptions<CommunicatorDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<CompanyUser> CompanyUsers { get; set; }
        public virtual DbSet<Component> Components { get; set; }
        public virtual DbSet<ComponentSubscriber> ComponentSubscribers { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<GroupMembership> GroupMemberships { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<RegisterUser> RegisterUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("ConnectionStrings:PC-DOM");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Polish_CI_AS");

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.ComponentId)
                    .HasName("PK__Comment__D79CF02E9A84814B");

                entity.ToTable("Comment");

                entity.Property(e => e.ComponentId)
                    .ValueGeneratedNever()
                    .HasColumnName("ComponentID");

                entity.Property(e => e.PostText)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.SourceId).HasColumnName("SourceID");

                entity.HasOne(d => d.Component)
                    .WithOne(p => p.CommentComponent)
                    .HasForeignKey<Comment>(d => d.ComponentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Comment__Compone__33D4B598");

                entity.HasOne(d => d.Source)
                    .WithMany(p => p.CommentSources)
                    .HasForeignKey(d => d.SourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Comment__SourceI__34C8D9D1");
            });

            modelBuilder.Entity<CompanyUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__CompanyU__1788CCAC888972F0");

                entity.ToTable("CompanyUser");

                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("UserID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.OwnerId).HasColumnName("OwnerID");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.CompanyUserOwners)
                    .HasForeignKey(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CompanyUs__Owner__35BCFE0A");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.CompanyUserUser)
                    .HasForeignKey<CompanyUser>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CompanyUs__UserI__36B12243");
            });

            modelBuilder.Entity<Component>(entity =>
            {
                entity.ToTable("Component");

                entity.Property(e => e.ComponentId)
                    .ValueGeneratedNever()
                    .HasColumnName("ComponentID");

                entity.Property(e => e.AuthorId).HasColumnName("AuthorID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ParentGroupId).HasColumnName("ParentGroupID");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Components)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Component__Autho__37A5467C");

                entity.HasOne(d => d.ParentGroup)
                    .WithMany(p => p.Components)
                    .HasForeignKey(d => d.ParentGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Component__Paren__38996AB5");
            });

            modelBuilder.Entity<ComponentSubscriber>(entity =>
            {
                entity.HasKey(e => new { e.ComponentId, e.UserId })
                    .HasName("PK__Componen__06E47CE4FD643E4C");

                entity.Property(e => e.ComponentId).HasColumnName("ComponentID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Component)
                    .WithMany(p => p.ComponentSubscribers)
                    .HasForeignKey(d => d.ComponentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Component__Compo__398D8EEE");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ComponentSubscribers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Component__UserI__3A81B327");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.Property(e => e.GroupId)
                    .ValueGeneratedNever()
                    .HasColumnName("GroupID");

                entity.Property(e => e.AuthorId).HasColumnName("AuthorID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.ParentGroupId).HasColumnName("ParentGroupID");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Groups__AuthorID__3D5E1FD2");

                entity.HasOne(d => d.ParentGroup)
                    .WithMany(p => p.InverseParentGroup)
                    .HasForeignKey(d => d.ParentGroupId)
                    .HasConstraintName("FK__Groups__ParentGr__3E52440B");
            });

            modelBuilder.Entity<GroupMembership>(entity =>
            {
                entity.HasKey(e => new { e.GroupId, e.UserId })
                    .HasName("PK__GroupMem__C5E27FC003D4116E");

                entity.ToTable("GroupMembership");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupMemberships)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GroupMemb__Group__3B75D760");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.GroupMemberships)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GroupMemb__UserI__3C69FB99");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.HasKey(e => e.ComponentId)
                    .HasName("PK__Image__D79CF02E11EDE4BF");

                entity.ToTable("Image");

                entity.Property(e => e.ComponentId)
                    .ValueGeneratedNever()
                    .HasColumnName("ComponentID");

                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.Component)
                    .WithOne(p => p.Image)
                    .HasForeignKey<Image>(d => d.ComponentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Image__Component__3F466844");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(e => e.ComponentId)
                    .HasName("PK__Post__D79CF02EB7823714");

                entity.ToTable("Post");

                entity.Property(e => e.ComponentId)
                    .ValueGeneratedNever()
                    .HasColumnName("ComponentID");

                entity.Property(e => e.PostText)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.Component)
                    .WithOne(p => p.Post)
                    .HasForeignKey<Post>(d => d.ComponentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Post__ComponentI__403A8C7D");
            });

            modelBuilder.Entity<RegisterUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Register__1788CCACBC21449D");

                entity.ToTable("RegisterUser");

                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("UserID");

                entity.Property(e => e.RegisterDate).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
