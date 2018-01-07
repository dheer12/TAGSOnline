namespace TAGS_BARCODE_WEBAPP_V4.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TagsDataModel : DbContext
    {
        public TagsDataModel()
            : base("name=TagsDataModel")
        {
        }

        public virtual DbSet<MEMBER_EVENT_CHECKINS> MEMBER_EVENT_CHECKINS { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TAGS_EVENTS> TAGS_EVENTS { get; set; }
        public virtual DbSet<TAGS_LOGIN> TAGS_LOGIN { get; set; }
        public virtual DbSet<TAGS_MEMBER> TAGS_MEMBER { get; set; }
        public virtual DbSet<TICKETED_CHECKINS> TICKETED_CHECKINS { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TAGS_EVENTS>()
                .HasMany(e => e.MEMBER_EVENT_CHECKINS)
                .WithRequired(e => e.TAGS_EVENTS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TAGS_LOGIN>()
                .Property(e => e.FIRST_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<TAGS_LOGIN>()
                .Property(e => e.LAST_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<TAGS_LOGIN>()
                .Property(e => e.PASSWORD)
                .IsUnicode(false);

            modelBuilder.Entity<TAGS_LOGIN>()
                .Property(e => e.USER_ROLE)
                .IsUnicode(false);

            modelBuilder.Entity<TAGS_LOGIN>()
                .HasMany(e => e.MEMBER_EVENT_CHECKINS)
                .WithOptional(e => e.TAGS_LOGIN)
                .HasForeignKey(e => e.UESR_ID);

            modelBuilder.Entity<TAGS_LOGIN>()
                .HasMany(e => e.TICKETED_CHECKINS)
                .WithOptional(e => e.TAGS_LOGIN)
                .HasForeignKey(e => e.STATION_1_USER_ID);

            modelBuilder.Entity<TAGS_MEMBER>()
                .HasMany(e => e.MEMBER_EVENT_CHECKINS)
                .WithRequired(e => e.TAGS_MEMBER)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TICKETED_CHECKINS>()
                .Property(e => e.TICKET_NUMBER)
                .IsUnicode(false);
        }
    }
}
