//using System;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata;
//using RatingSystem.Models;

//#nullable disable

//namespace RatingSystem.Data
//{
//    public partial class RatingDbContext : DbContext
//    {
//        public RatingDbContext(DbContextOptions<RatingDbContext> options)
//            : base(options)
//        {
//        }

//        public virtual DbSet<ConferenceXAtendee> ConferenceXAtendees { get; set; }


//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

//            modelBuilder.Entity<ConferenceXAtendee>(entity =>
//            {

//                entity.ToTable("ConferenceXAttendee");
//                entity.Property(e => e.AttendeeEmail)
//                   .IsRequired()
//                   .HasMaxLength(50);

//                entity.Property(e => e.ConferenceId)
//                    .IsRequired();

//                entity.Property(e => e.StatusId)
//                    .IsRequired();

//                entity.Property(e => e.Rating).HasColumnType("decimal(18, 0)");

//            });
//            OnModelCreatingPartial(modelBuilder);
//        }

//        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
//    }
//}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RatingSystem.Models;


#nullable disable

namespace RatingSystem.Data
{
    public partial class RatingDbContext : DbContext
    {

        public RatingDbContext(DbContextOptions<RatingDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<AverageRating> AverageRatings { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.HasKey(c => new { c.ExternalId, c.UserId, c.Category });

                entity.ToTable("Rating");

                entity.Property(e => e.Category)
                    .IsRequired();
                entity.Property(e => e.ExternalId)
                    .IsRequired();
                entity.Property(e => e.RatingValue)
                    .IsRequired();
                entity.Property(e => e.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<AverageRating>(entity =>
            {
                entity.HasKey(c => new { c.ExternalId, c.Category});

                entity.ToTable("AverageRating");

                entity.Property(e => e.AverageRatingValue)
                    .IsRequired();

                entity.Property(e => e.Category);
                
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
