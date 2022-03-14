﻿// <auto-generated />
using System;
using LibraryEntities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LibraryEntities.Migrations
{
    [DbContext(typeof(LibraryContext))]
    [Migration("20220304165302_firstm")]
    partial class firstm
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("LibraryEntities.Models.Author", b =>
                {
                    b.Property<int>("AuthorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("authorId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AuthorId"), 1L, 1);

                    b.Property<string>("AuthorCountry")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("authorCountry");

                    b.Property<string>("AuthorName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("authorName");

                    b.HasKey("AuthorId");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("LibraryEntities.Models.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("bookId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookId"), 1L, 1);

                    b.Property<int>("AuthorId")
                        .HasColumnType("int")
                        .HasColumnName("authorId");

                    b.Property<int>("BookCopys")
                        .HasColumnType("int")
                        .HasColumnName("bookCopys");

                    b.Property<string>("BookLanguage")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("bookLanguage");

                    b.Property<string>("BookName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("bookName");

                    b.Property<int>("BookNumOfPages")
                        .HasColumnType("int")
                        .HasColumnName("bookNumOfPages");

                    b.Property<int>("BookPublishedYear")
                        .HasColumnType("int")
                        .HasColumnName("bookPublishedYear");

                    b.Property<int>("GenreId")
                        .HasColumnType("int")
                        .HasColumnName("genreId");

                    b.HasKey("BookId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("GenreId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("LibraryEntities.Models.Borrowing", b =>
                {
                    b.Property<int>("BorrowingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("borrowingId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BorrowingId"), 1L, 1);

                    b.Property<int>("BorrowingBookId")
                        .HasColumnType("int")
                        .HasColumnName("borrowingBookId");

                    b.Property<DateTime>("BorrowingDate")
                        .HasColumnType("date")
                        .HasColumnName("borrowingDate");

                    b.Property<DateTime>("BorrowingReturnDate")
                        .HasColumnType("date")
                        .HasColumnName("borrowingReturnDate");

                    b.Property<DateTime?>("BorrowingReturnedDate")
                        .HasColumnType("date")
                        .HasColumnName("borrowingReturnedDate");

                    b.Property<int>("BorrowingUserId")
                        .HasColumnType("int")
                        .HasColumnName("borrowingUserId");

                    b.HasKey("BorrowingId");

                    b.HasIndex("BorrowingBookId");

                    b.HasIndex("BorrowingUserId");

                    b.ToTable("Borrowings");
                });

            modelBuilder.Entity("LibraryEntities.Models.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("genreId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GenreId"), 1L, 1);

                    b.Property<string>("GenreName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("genreName");

                    b.HasKey("GenreId");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("LibraryEntities.Models.User", b =>
                {
                    b.Property<int>("UsersId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UsersId"), 1L, 1);

                    b.Property<string>("UserTzId")
                        .IsRequired()
                        .HasMaxLength(9)
                        .IsUnicode(false)
                        .HasColumnType("varchar(9)");

                    b.Property<string>("UsersAddress")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("UsersEmail")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("UsersFirstName")
                        .IsRequired()
                        .HasMaxLength(13)
                        .IsUnicode(false)
                        .HasColumnType("varchar(13)");

                    b.Property<string>("UsersLastName")
                        .IsRequired()
                        .HasMaxLength(13)
                        .IsUnicode(false)
                        .HasColumnType("varchar(13)");

                    b.Property<string>("UsersPassword")
                        .IsRequired()
                        .HasMaxLength(16)
                        .IsUnicode(false)
                        .HasColumnType("varchar(16)");

                    b.Property<string>("UsersPhone")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("UsersRole")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("UsersUserName")
                        .IsRequired()
                        .HasMaxLength(11)
                        .IsUnicode(false)
                        .HasColumnType("varchar(11)");

                    b.HasKey("UsersId")
                        .HasName("PK_Borrowers");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LibraryEntities.Models.Book", b =>
                {
                    b.HasOne("LibraryEntities.Models.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .IsRequired()
                        .HasConstraintName("FK_Books_Authors");

                    b.HasOne("LibraryEntities.Models.Genre", "Genre")
                        .WithMany("Books")
                        .HasForeignKey("GenreId")
                        .IsRequired()
                        .HasConstraintName("FK_Books_Genres");

                    b.Navigation("Author");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("LibraryEntities.Models.Borrowing", b =>
                {
                    b.HasOne("LibraryEntities.Models.Book", "BorrowingBook")
                        .WithMany("Borrowings")
                        .HasForeignKey("BorrowingBookId")
                        .IsRequired()
                        .HasConstraintName("FK_Borrowings_Books");

                    b.HasOne("LibraryEntities.Models.User", "BorrowingUser")
                        .WithMany("Borrowings")
                        .HasForeignKey("BorrowingUserId")
                        .IsRequired()
                        .HasConstraintName("FK_Borrowings_Borrowers");

                    b.Navigation("BorrowingBook");

                    b.Navigation("BorrowingUser");
                });

            modelBuilder.Entity("LibraryEntities.Models.Author", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("LibraryEntities.Models.Book", b =>
                {
                    b.Navigation("Borrowings");
                });

            modelBuilder.Entity("LibraryEntities.Models.Genre", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("LibraryEntities.Models.User", b =>
                {
                    b.Navigation("Borrowings");
                });
#pragma warning restore 612, 618
        }
    }
}
