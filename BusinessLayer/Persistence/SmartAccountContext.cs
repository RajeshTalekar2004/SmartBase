using Microsoft.EntityFrameworkCore;
using SmartBase.BusinessLayer.Core.Domain;
using System;

#nullable disable

namespace SmartBase.BusinessLayer.Persistence
{
    public partial class SmartAccountContext : DbContext
    {
        public SmartAccountContext()
        {
        }

        public SmartAccountContext(DbContextOptions<SmartAccountContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountMaster> AccountMasters { get; set; }
        public virtual DbSet<BillDetail> BillDetails { get; set; }
        public virtual DbSet<BillMaster> BillMasters { get; set; }
        public virtual DbSet<CgstMaster> CgstMasters { get; set; }
        public virtual DbSet<CompInfo> CompInfos { get; set; }
        public virtual DbSet<Ledger> Ledgers { get; set; }
        public virtual DbSet<SgstMaster> SgstMasters { get; set; }
        public virtual DbSet<TransactionMaster> TransactionMasters { get; set; }
        public virtual DbSet<TypeMaster> TypeMasters { get; set; }
        public virtual DbSet<UserInfo> UserInfos { get; set; }
        public virtual DbSet<VoucherDetail> VoucherDetails { get; set; }
        public virtual DbSet<VoucherMaster> VoucherMasters { get; set; }
        public virtual DbSet<IgstMaster> IgstMasters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=US1139203W1\\SQLEXPRESSLOCAL;Database=SmartAccount;User Id=sa;Password=Yashraj1234!@#$;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AccountMaster>(entity =>
            {
                entity.HasKey(e => new { e.CompCode, e.AccYear, e.AccountId })
                .HasName("PK_AccountMaster");

                entity.ToTable("AccountMaster");

                entity.Property(e => e.CompCode)
                    .HasMaxLength(2)
                    .IsFixedLength(true);

                entity.Property(e => e.AccYear)
                    .HasMaxLength(4)
                    .IsFixedLength(true);

                entity.Property(e => e.AccountId)
                    .HasMaxLength(16)
                    .IsFixedLength(true);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Closing).HasColumnType("money");

                entity.Property(e => e.CurCr).HasColumnType("money");

                entity.Property(e => e.CurDr).HasColumnType("money");

                entity.Property(e => e.Opening).HasColumnType("money");
            });

            modelBuilder.Entity<BillDetail>(entity =>
            {
                entity.HasKey(e => new { e.CompCode, e.AccYear, e.BillId, e.ItemSr })
                .HasName("PK_BillDetail");

                entity.ToTable("BillDetail");

                entity.Property(e => e.CompCode)
                    .HasMaxLength(2)
                    .IsFixedLength(true);

                entity.Property(e => e.AccYear)
                    .HasMaxLength(4)
                    .IsFixedLength(true);

                entity.Property(e => e.BillId)
                    .HasMaxLength(16)
                    .IsFixedLength(true);

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.VouNo)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.HasOne(d => d.BillMaster)
                    .WithMany(p => p.BillDetails)
                    .HasForeignKey(d => new { d.CompCode, d.AccYear, d.BillId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BillDetail_BillMaster");
            });

            modelBuilder.Entity<BillMaster>(entity =>
            {
                entity.HasKey(e => new { e.CompCode, e.AccYear, e.BillId })
                    .HasName("PK_BillMaster");

                entity.ToTable("BillMaster");

                entity.Property(e => e.CompCode)
                    .HasMaxLength(2)
                    .IsFixedLength(true);

                entity.Property(e => e.AccYear)
                    .HasMaxLength(4)
                    .IsFixedLength(true);

                entity.Property(e => e.BillId)
                    .HasMaxLength(16)
                    .IsFixedLength(true);

                entity.Property(e => e.AccountId)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsFixedLength(true);

                entity.Property(e => e.Adjusted).HasColumnType("money");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Balance).HasColumnType("money");

                entity.Property(e => e.BillDate).HasColumnType("date");
            });

            modelBuilder.Entity<CgstMaster>(entity =>
            {
                entity.HasKey(e => e.CgstId).HasName("PK_CgstMaster");

                entity.ToTable("CgstMaster");

                entity.Property(e => e.CgstId).ValueGeneratedNever();

                entity.Property(e => e.CgstDetail)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CgstRate).HasColumnType("money");
            });

            modelBuilder.Entity<CompInfo>(entity =>
            {
                entity.HasKey(e => new { e.CompCode, e.AccYear }).HasName("PK_CompInfo");

                entity.ToTable("CompInfo");

                entity.Property(e => e.CompCode)
                    .HasMaxLength(2)
                    .IsFixedLength(true);

                entity.Property(e => e.AccYear)
                    .HasMaxLength(4)
                    .IsFixedLength(true);

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsFixedLength(true);

                entity.Property(e => e.AutoVoucher)
                    .HasMaxLength(1)
                    .IsFixedLength(true);

                entity.Property(e => e.BillMatch)
                    .HasMaxLength(1)
                    .IsFixedLength(true);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TaxId).HasMaxLength(50);

                entity.Property(e => e.YearBegin).HasColumnType("date");

                entity.Property(e => e.YearEnd).HasColumnType("date");
            });


            modelBuilder.Entity<IgstMaster>(entity =>
            {
                entity.HasKey(e => e.IgstId).HasName("PK_IgstMaster");

                entity.ToTable("IgstMaster");

                entity.Property(e => e.IgstId).ValueGeneratedNever();

                entity.Property(e => e.IgstDetail)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IgstRate).HasColumnType("money");
            });

            modelBuilder.Entity<Ledger>(entity =>
            {
                entity.HasKey(e => new { e.CompCode, e.AccYear, e.VouNo, e.ItemSr })
                      .HasName("PK_Ledger");

                entity.ToTable("Ledger");

                entity.Property(e => e.ItemSr).ValueGeneratedNever();

                entity.Property(e => e.AccYear)
                    .HasMaxLength(4)
                    .IsFixedLength(true);

                entity.Property(e => e.AccountId)
                    .HasMaxLength(16)
                    .IsFixedLength(true);

                entity.Property(e => e.TrxType)
                    .HasMaxLength(2)
                    .IsFixedLength(true);

                entity.Property(e => e.BilChq)
                    .HasMaxLength(16)
                    .IsFixedLength(true);

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.CompCode)
                    .HasMaxLength(2)
                    .IsFixedLength(true);

                entity.Property(e => e.CorrAccountId)
                    .HasMaxLength(16)
                    .IsFixedLength(true);

                entity.Property(e => e.DrCr)
                    .HasMaxLength(1)
                    .IsFixedLength(true);

                entity.Property(e => e.VouDate).HasColumnType("date");

                entity.Property(e => e.VouDetail)
                    .HasMaxLength(50)
                    .IsFixedLength(true);

                entity.Property(e => e.VouNo)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.HasOne(d => d.VoucherMaster)
                    .WithMany(p => p.Ledgers)
                    .HasForeignKey(d => new { d.CompCode, d.AccYear, d.VouNo })
                    .HasConstraintName("FK_Ledger_VoucherMaster");
            });

            modelBuilder.Entity<SgstMaster>(entity =>
            {
                entity.HasKey(e => e.SgstId).HasName("PK_SgstMaster");

                entity.ToTable("SgstMaster");

                entity.Property(e => e.SgstId).ValueGeneratedNever();

                entity.Property(e => e.SgstDetail)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SgstRate).HasColumnType("money");
            });




            modelBuilder.Entity<TransactionMaster>(entity =>
            {
                entity.HasKey(e => e.TrxId).HasName("PK_TransactionMaster");

                entity.ToTable("TransactionMaster");

                entity.Property(e => e.TrxId)
                    .HasMaxLength(1)
                    .IsFixedLength(true);

                entity.Property(e => e.DrCr)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsFixedLength(true);

                entity.Property(e => e.TrxDetail).HasMaxLength(50);

                entity.Property(e => e.AccountId1)
                    .HasMaxLength(16)
                    .IsFixedLength(true);

                entity.Property(e => e.AccountId2)
                    .HasMaxLength(16)
                    .IsFixedLength(true);

                entity.Property(e => e.AccountId3)
                    .HasMaxLength(16)
                    .IsFixedLength(true);

            });

            modelBuilder.Entity<TypeMaster>(entity =>
            {
                entity.HasKey(e => new { e.CompCode, e.AccYear, e.TrxCd })
                     .HasName("PK_TypeMaster");

                entity.ToTable("TypeMaster");

                entity.Property(e => e.CompCode)
                    .HasMaxLength(2)
                    .IsFixedLength(true);

                entity.Property(e => e.AccYear)
                    .HasMaxLength(4)
                    .IsFixedLength(true);

                entity.Property(e => e.TrxCd)
                    .HasMaxLength(1)
                    .IsFixedLength(true);

                entity.Property(e => e.Prefix)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsFixedLength(true);

                entity.Property(e => e.TrxDetail)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.TrxCdNavigation)
                    .WithMany(p => p.TypeMasters)
                    .HasForeignKey(d => d.TrxCd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TypeMaster_TransactionMaster");
            });

            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.HasKey(e => e.UserName)
                    .HasName("PK_UserInfo");

                entity.ToTable("UserInfo");

                entity.Property(e => e.UserName)
                    .HasMaxLength(25)
                    .IsFixedLength(true);

                entity.Property(e => e.CompCode)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsFixedLength(true);

                entity.Property(e => e.UserEmailId).HasMaxLength(50);

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<VoucherDetail>(entity =>
            {
                entity.HasKey(e => new { e.CompCode, e.AccYear, e.VouNo, e.ItemSr })
                    .HasName("PK_VoucherDetail");

                entity.ToTable("VoucherDetail");

                entity.Property(e => e.CompCode)
                    .HasMaxLength(2)
                    .IsFixedLength(true);

                entity.Property(e => e.AccYear)
                    .HasMaxLength(4)
                    .IsFixedLength(true);

                entity.Property(e => e.VouNo)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.AccountId)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsFixedLength(true);

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.DrCr)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsFixedLength(true);

                entity.Property(e => e.VouDetail).HasMaxLength(50);

                entity.HasOne(d => d.AccountMaster)
                    .WithMany(p => p.VoucherDetails)
                    .HasForeignKey(d => new { d.CompCode, d.AccYear, d.AccountId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VoucherDetail_AccountMaster");

                entity.HasOne(d => d.VoucherMaster)
                    .WithMany(p => p.VoucherDetails)
                    .HasForeignKey(d => new { d.CompCode, d.AccYear, d.VouNo })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VoucherDetail_VoucherMaster");
            });

            modelBuilder.Entity<VoucherMaster>(entity =>
            {
                entity.HasKey(e => new { e.CompCode, e.AccYear, e.VouNo })
                    .HasName("PK_VoucherMaster");

                entity.ToTable("VoucherMaster");

                entity.Property(e => e.CompCode)
                    .HasMaxLength(2)
                    .IsFixedLength(true);

                entity.Property(e => e.AccYear)
                    .HasMaxLength(4)
                    .IsFixedLength(true);

                entity.Property(e => e.VouNo)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.AccountId)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsFixedLength(true);

                entity.Property(e => e.BilChq)
                    .HasMaxLength(16)
                    .IsFixedLength(true);

                entity.Property(e => e.BillId)
                    .HasMaxLength(16)
                    .IsFixedLength(true);

                entity.Property(e => e.CgstAmount).HasColumnType("money");

                entity.Property(e => e.DrCr)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsFixedLength(true);

                entity.Property(e => e.VouAmount).HasColumnType("money");

                entity.Property(e => e.NetAmount).HasColumnType("money");

                entity.Property(e => e.SgstAmount).HasColumnType("money");

                entity.Property(e => e.IgstAmount).HasColumnType("money");

                entity.Property(e => e.TrxType)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsFixedLength(true);

                entity.Property(e => e.VouDate).HasColumnType("date");

                entity.Property(e => e.VouDetail).HasMaxLength(50);

                entity.HasOne(d => d.Cgst)
                    .WithMany(p => p.VoucherMasters)
                    .HasForeignKey(d => d.CgstId)
                    .HasConstraintName("FK_VoucherMaster_CgstMaster");

                entity.HasOne(d => d.Sgst)
                    .WithMany(p => p.VoucherMasters)
                    .HasForeignKey(d => d.SgstId)
                    .HasConstraintName("FK_VoucherMaster_SgstMaster");

                entity.HasOne(d => d.Igst)
                    .WithMany(p => p.VoucherMasters)
                    .HasForeignKey(d => d.IgstId)
                    .HasConstraintName("FK_VoucherMaster_IgstMaster");

                entity.HasOne(d => d.CompInfo)
                    .WithMany(p => p.VoucherMasters)
                    .HasForeignKey(d => new { d.CompCode, d.AccYear })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VoucherMaster_CompInfo");

                entity.HasOne(d => d.AccountMaster)
                    .WithMany(p => p.VoucherMasters)
                    .HasForeignKey(d => new { d.CompCode, d.AccYear, d.AccountId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VoucherMaster_AccountMaster");

                entity.HasOne(d => d.BillMaster)
                    .WithMany(p => p.VoucherMasters)
                    .HasForeignKey(d => new { d.CompCode, d.AccYear, d.BillId })
                    .HasConstraintName("FK_VoucherMaster_BillMaster");

                entity.HasOne(d => d.TypeMaster)
                    .WithMany(p => p.VoucherMasters)
                    .HasForeignKey(d => new { d.CompCode, d.AccYear, d.TrxType })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VoucherMaster_TypeMaster");
            });


            modelBuilder.Seed();

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        
    }
}
