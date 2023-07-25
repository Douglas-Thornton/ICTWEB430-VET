using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using VETAPPAPI.Data.Interface;
using VETAPPAPI.Models;

namespace VETAPPAPI.Data
{
    public class VETDBContext : DbContext, IVETDBContext
    {
        private readonly IConfiguration configuration;
        public DbSet<User> Users { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<InvitedUser> InvitedUsers { get; set; }
        public DbSet<InvitedPet> InvitedPets { get; set; }


        public VETDBContext(DbContextOptions<VETDBContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Gets the connection string from program.cs. 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("VetDB"));
            }
        }

        /// <summary>
        /// Save changes to the model.
        /// </summary>
        /// <returns>The number of objects written to the underlying database.</returns>
        public override int SaveChanges()
        {
            // Set audit fields before saving changes
            return base.SaveChanges();
        }

        /// <summary>
        /// Save changes to the model async.
        /// </summary>
        /// <returns>The number of objects written to the underlying database.</returns>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Set audit fields before saving changes
            return base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Configure the model.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<InvitedPet>(entity =>
            //{
            //    entity.ToTable("InvitedPet");

            //    entity.HasKey(e => e.InvitedPetID);

            //    entity.Property(e => e.InvitedPetID).HasColumnName("InvitedPetID");

            //    entity.Property(e => e.PetID).HasColumnName("PetID");

            //    entity.Property(e => e.MeetingID).HasColumnName("MeetingID");

            //    entity.Property(e => e.InviteID).HasColumnName("InviteID");

            //    entity.HasOne(d => d.InvitedUser)
            //        .WithMany()
            //        .HasForeignKey(d => d.InviteID);

            //    entity.HasOne(d => d.Meeting)
            //        .WithMany()
            //        .HasForeignKey(d => d.MeetingID);

            //    entity.HasOne(d => d.Pet)
            //        .WithMany()
            //        .HasForeignKey(d => d.PetID);
            //});

            //modelBuilder.Entity<InvitedUser>(entity =>
            //{
            //    entity.ToTable("InvitedUser");

            //    entity.HasKey(e => e.InviteID);

            //    entity.Property(e => e.InviteID).HasColumnName("InviteID");

            //    entity.Property(e => e.UserID).HasColumnName("UserID");

            //    entity.Property(e => e.MeetingID).HasColumnName("MeetingID");

            //    entity.Property(e => e.Accepted).HasColumnName("Accepted");

            //    entity.Property(e => e.ResponseDate).HasColumnName("ResponseDate");

            //    entity.HasOne(d => d.User)
            //        .WithMany()
            //        .HasForeignKey(d => d.UserID);

            //    entity.HasOne(d => d.Meeting)
            //        .WithMany()
            //        .HasForeignKey(d => d.MeetingID);
            //});

            //modelBuilder.Entity<Meeting>(entity =>
            //{
            //    entity.ToTable("Meeting");

            //    entity.HasKey(e => e.MeetingID);

            //    entity.Property(e => e.MeetingID).HasColumnName("MeetingID");

            //    entity.Property(e => e.UserCreated).HasColumnName("UserCreated");

            //    entity.Property(e => e.MeetingDate).HasColumnName("MeetingDate");

            //    entity.Property(e => e.MeetingLocation).HasColumnName("MeetingLocation");

            //    entity.Property(e => e.MeetingCreationDate).HasColumnName("MeetingCreationDate");

            //    entity.Property(e => e.MeetingCancellationDate).HasColumnName("MeetingCancellationDate");

            //    entity.Property(e => e.MeetingName).HasColumnName("MeetingName");

            //    entity.Property(e => e.MeetingMessage).HasColumnName("MeetingMessage");

            //    entity.HasOne(d => d.User)
            //        .WithMany()
            //        .HasForeignKey(d => d.UserCreated);
            //});

            //modelBuilder.Entity<Pet>(entity =>
            //{
            //    entity.ToTable("Pet");

            //    entity.HasKey(e => e.PetID);

            //    entity.Property(e => e.PetID).HasColumnName("PetID");

            //    entity.Property(e => e.OwnerID).HasColumnName("OwnerID");

            //    entity.Property(e => e.PetName).HasColumnName("PetName");

            //    entity.Property(e => e.PetBreed).HasColumnName("PetBreed");

            //    entity.Property(e => e.PetAge).HasColumnName("PetAge");

            //    entity.Property(e => e.PetGender).HasColumnName("PetGender");

            //    entity.Property(e => e.PetPhotoFileLocation).HasColumnName("PetPhotoFileLocation");

            //    entity.Property(e => e.PetDiscoverability).HasColumnName("PetDiscoverability");

            //    entity.HasOne(d => d.Owner)
            //        .WithMany()
            //        .HasForeignKey(d => d.OwnerID);
            //});

            //modelBuilder.Entity<User>(entity =>
            //{
            //    entity.ToTable("User");

            //    entity.HasKey(e => e.UserID);

            //    entity.Property(e => e.UserID).HasColumnName("UserID");

            //    entity.Property(e => e.FirstName).HasColumnName("FirstName");

            //    entity.Property(e => e.Surname).HasColumnName("Surname");

            //    entity.Property(e => e.PhoneNumber).HasColumnName("PhoneNumber");

            //    entity.Property(e => e.Email).HasColumnName("Email");

            //    entity.Property(e => e.Suburb).HasColumnName("Suburb");

            //    entity.Property(e => e.Postcode).HasColumnName("Postcode");

            //    entity.Property(e => e.LoginUsername).HasColumnName("LoginUsername");

            //    entity.Property(e => e.LoginPassword).HasColumnName("LoginPassword");

            //    entity.Property(e => e.WebpageAnimalPreference).HasColumnName("WebpageAnimalPreference");
            //});
        }


        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
