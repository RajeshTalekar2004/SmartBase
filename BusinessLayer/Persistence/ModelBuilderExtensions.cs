using Microsoft.EntityFrameworkCore;
using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Services;
using System;

namespace SmartBase.BusinessLayer.Persistence
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {

            //CGST
            modelBuilder.Entity<CgstMaster>().HasData(
                 new CgstMaster
                 {
                     CgstId = 1,
                     CgstDetail = "CGST-1.50%-SALES.",
                     CgstRate = (decimal)1.5
                 });

            modelBuilder.Entity<CgstMaster>().HasData(
                 new CgstMaster
                 {
                     CgstId = 51,
                     CgstDetail = "CGST-9%GOODS-MATERIAL PURCHASE",
                     CgstRate = (decimal)9
                 });

            modelBuilder.Entity<CgstMaster>().HasData(
                 new CgstMaster
                 {
                     CgstId = 53,
                     CgstDetail = "CGST-1.5% GOODS-MATERIAL PURCHA",
                     CgstRate = (decimal)1.5
                 });

            modelBuilder.Entity<CgstMaster>().HasData(
                 new CgstMaster
                 {
                     CgstId = 55,
                     CgstDetail = "CGST-9%-SERVICE-LABOUR CHARGES",
                     CgstRate = (decimal)9
                 });

            modelBuilder.Entity<CgstMaster>().HasData(
                 new CgstMaster
                 {
                     CgstId = 57,
                     CgstDetail = "CGST-6%-SERVICE-LABOUR CHARGES",
                     CgstRate = (decimal)6
                 });

            modelBuilder.Entity<CgstMaster>().HasData(
                 new CgstMaster
                 {
                     CgstId = 59,
                     CgstDetail = "CGST-2.5%SERVICE-LABOUR CHARGE",
                     CgstRate = (decimal)2.5
                 });

            modelBuilder.Entity<CgstMaster>().HasData(
                 new CgstMaster
                 {
                     CgstId = 61,
                     CgstDetail = "CGST-14%GOODS-MATERIAL PURCHAS",
                     CgstRate = (decimal)14
                 });

            modelBuilder.Entity<CgstMaster>().HasData(
                 new CgstMaster
                 {
                     CgstId = 63,
                     CgstDetail = "CGST-6%GOODS-MATERIAL PURCHASE",
                     CgstRate = (decimal)6
                 });

            modelBuilder.Entity<CgstMaster>().HasData(
                 new CgstMaster
                 {
                     CgstId = 65,
                     CgstDetail = "CGST-2.5%GOODS-MATERIAL PURCHA",
                     CgstRate = (decimal)2.5
                 });

            modelBuilder.Entity<CgstMaster>().HasData(
                 new CgstMaster
                 {
                     CgstId = 66,
                     CgstDetail = "CGST-0.125%GOODS-MATERIAL PURC",
                     CgstRate = (decimal)0.12
                 });

            //SGST

            modelBuilder.Entity<SgstMaster>().HasData(
                 new SgstMaster
                 {
                     SgstId = 2,
                     SgstDetail = "SGST-1.50%-SALES.",
                     SgstRate = (decimal)1.5
                 });

            modelBuilder.Entity<SgstMaster>().HasData(
                 new SgstMaster
                 {
                     SgstId = 52,
                     SgstDetail = "SGST-9%GOODS-MATERIAL PURCHASE",
                     SgstRate = (decimal)1.5
                 });

            modelBuilder.Entity<SgstMaster>().HasData(
                 new SgstMaster
                 {
                     SgstId = 54,
                     SgstDetail = "SGST-1.5%GOODS-MATERIAL PURCHA",
                     SgstRate = (decimal)1.5
                 });

            modelBuilder.Entity<SgstMaster>().HasData(
                 new SgstMaster
                 {
                     SgstId = 56,
                     SgstDetail = "SGST-9%-SERVICE-LABOUR CHARGES",
                     SgstRate = (decimal)1.5
                 });

            modelBuilder.Entity<SgstMaster>().HasData(
                 new SgstMaster
                 {
                     SgstId = 58,
                     SgstDetail = "SGST-6%-SERVICE-LABOUR CHARGES",
                     SgstRate = (decimal)1.5
                 });

            modelBuilder.Entity<SgstMaster>().HasData(
                 new SgstMaster
                 {
                     SgstId = 60,
                     SgstDetail = "SGST-2.5%SERVICE-LABOUR CHARGE",
                     SgstRate = (decimal)1.5
                 });

            modelBuilder.Entity<SgstMaster>().HasData(
                 new SgstMaster
                 {
                     SgstId = 62,
                     SgstDetail = "SGST-14%GOODS-MATERIAL PURCHAS",
                     SgstRate = (decimal)14
                 });

            modelBuilder.Entity<SgstMaster>().HasData(
                 new SgstMaster
                 {
                     SgstId = 64,
                     SgstDetail = "SGST-6%GOODS-MATERIAL PURCHASE",
                     SgstRate = (decimal)6
                 });

            modelBuilder.Entity<SgstMaster>().HasData(
                 new SgstMaster
                 {
                     SgstId = 66,
                     SgstDetail = "SGST-2.5%GOODS-MATERIAL PURCHA",
                     SgstRate = (decimal)2.5
                 });

            modelBuilder.Entity<SgstMaster>().HasData(
                 new SgstMaster
                 {
                     SgstId = 67,
                     SgstDetail = "SGST-.125%GOODS-MATERIAL PURCH",
                     SgstRate = (decimal)0.12
                 });

            //Company
            modelBuilder.Entity<TransactionMaster>().HasData(
                new TransactionMaster
                {
                    TrxId = "A",
                    DrCr = "1",
                    TrxDetail = "BANK RECEIPTS",
                    AccountId1 = "503",
                    AccountId2 = "604",
                    AccountId3 = null
                });

            modelBuilder.Entity<TransactionMaster>().HasData(
                new TransactionMaster
                {
                    TrxId = "B",
                    DrCr = "2",
                    TrxDetail = "BANK PAYMENTS",
                    AccountId1 = "605",
                    AccountId2 = "503",
                    AccountId3 = "604"
                });
            modelBuilder.Entity<TransactionMaster>().HasData(
                new TransactionMaster
                {
                    TrxId = "C",
                    DrCr = "2",
                    TrxDetail = "CASH PAYMENTS",
                    AccountId1 = "605",
                    AccountId2 = "503",
                    AccountId3 = "604"
                });
            modelBuilder.Entity<TransactionMaster>().HasData(
                new TransactionMaster
                {
                    TrxId = "D",
                    DrCr = "1",
                    TrxDetail = "CASH RECEIPTS",
                    AccountId1 = "605",
                    AccountId2 = "503",
                    AccountId3 = "604"
                });
            modelBuilder.Entity<TransactionMaster>().HasData(
                new TransactionMaster
                {
                    TrxId = "S",
                    DrCr = "1",
                    TrxDetail = "SALES",
                    AccountId1 = "60302",
                    AccountId2 = "50501",
                    AccountId3 = "604"
                });
            modelBuilder.Entity<TransactionMaster>().HasData(
                new TransactionMaster
                {
                    TrxId = "P",
                    DrCr = "2",
                    TrxDetail = "PURCHASES",
                    AccountId1 = "50501",
                    AccountId2 = "60302",
                    AccountId3 = null
                });
            modelBuilder.Entity<TransactionMaster>().HasData(
                new TransactionMaster
                {
                    TrxId = "N",
                    DrCr = "1",
                    TrxDetail = "DEBIT NOTES",
                    AccountId1 = "60302",
                    AccountId2 = "50501",
                    AccountId3 = null
                });
            modelBuilder.Entity<TransactionMaster>().HasData(
                new TransactionMaster
                {
                    TrxId = "M",
                    DrCr = "2",
                    TrxDetail = "CREDIT NOTES",
                    AccountId1 = "60302",
                    AccountId2 = "50501",
                    AccountId3 = null
                });
            modelBuilder.Entity<TransactionMaster>().HasData(
                new TransactionMaster
                {
                    TrxId = "J",
                    DrCr = "1",
                    TrxDetail = "Journal Voucher",
                    AccountId1 = null,
                    AccountId2 = null,
                    AccountId3 = null
                });

            //TypeMaster
             modelBuilder.Entity<TypeMaster>().HasData(
                new TypeMaster
                {
                    CompCode = "01",
                    AccYear = "1920",
                    TrxCd = "A",
                    TrxDetail = "BANK RECEIPTS",
                    Prefix = "1920",
                    ItemSr = null
                });
            modelBuilder.Entity<TypeMaster>().HasData(
                new TypeMaster
                {
                    CompCode = "01",
                    AccYear = "1920",
                    TrxCd = "B",
                    TrxDetail = "BANK PAYMENTS",
                    Prefix = "1920",
                    ItemSr = null
                });
            modelBuilder.Entity<TypeMaster>().HasData(
                new TypeMaster
                {
                    CompCode = "01",
                    AccYear = "1920",
                    TrxCd = "C",
                    TrxDetail = "CASH PAYMENTS",
                    Prefix = "1920",
                    ItemSr = null
                });
            modelBuilder.Entity<TypeMaster>().HasData(
                new TypeMaster
                {
                    CompCode = "01",
                    AccYear = "1920",
                    TrxCd = "D",
                    TrxDetail = "CASH RECEIPTS",
                    Prefix = "1920",
                    ItemSr = null
                });
            modelBuilder.Entity<TypeMaster>().HasData(
                new TypeMaster
                {
                    CompCode = "01",
                    AccYear = "1920",
                    TrxCd = "S",
                    TrxDetail = "SALES",
                    Prefix = "1920",
                    ItemSr = null
                });
            modelBuilder.Entity<TypeMaster>().HasData(
                new TypeMaster
                {
                    CompCode = "01",
                    AccYear = "1920",
                    TrxCd = "P",
                    TrxDetail = "PURCHASES",
                    Prefix = "1920",
                    ItemSr = null
                });
            modelBuilder.Entity<TypeMaster>().HasData(
                new TypeMaster
                {
                    CompCode = "01",
                    AccYear = "1920",
                    TrxCd = "N",
                    TrxDetail = "DEBIT NOTES",
                    Prefix = "1920",
                    ItemSr = null
                });
            modelBuilder.Entity<TypeMaster>().HasData(
                new TypeMaster
                {
                    CompCode = "01",
                    AccYear = "1920",
                    TrxCd = "M",
                    TrxDetail = "CREDIT NOTES",
                    Prefix = "1920",
                    ItemSr = null
                });
            modelBuilder.Entity<TypeMaster>().HasData(
                new TypeMaster
                {
                    CompCode = "01",
                    AccYear = "1920",
                    TrxCd = "J",
                    TrxDetail = "Journal Voucher",
                    Prefix = "1920",
                    ItemSr = null
                });

            //Company
            modelBuilder.Entity<CompInfo>().HasData(
                new CompInfo
                {
                    CompCode = "01",
                    AccYear = "1920",
                    Name = "Default Company",
                    YearBegin = DateTime.Parse("04/01/2019"),
                    YearEnd = DateTime.Parse("03/31/2020"),
                    TaxId = "G.S.T.Number",
                    AutoVoucher = "Y",
                    BillMatch = "Y",
                    Address = "Company Address"
                });

            //User info
            PasswordService passwordService = new PasswordService();
            string password = "passsword";
            var salt = passwordService.SaltCreate();
            var hash = passwordService.HashCreate(password, salt);

            modelBuilder.Entity<UserInfo>().HasData(
                 new UserInfo
                 {
                     CompCode = "01",
                     UserName = "rajesh",
                     UserEmailId = "rajeshtalekar@gmail.com",
                     UserPassword = hash,
                     UserSalt = salt
                 });

            modelBuilder.Entity<UserInfo>().HasData(
                 new UserInfo
                 {
                     CompCode = "01",
                     UserName = "suyash",
                     UserEmailId = "suyashtalekar@gmail.com",
                     UserPassword = hash,
                     UserSalt = salt
                 });

        }
    }
}
