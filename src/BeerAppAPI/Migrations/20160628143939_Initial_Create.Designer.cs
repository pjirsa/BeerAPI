using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BeerAppAPI.Models;

namespace BeerAppAPI.Migrations
{
    [DbContext(typeof(BeerDBContext))]
    [Migration("20160628143939_Initial_Create")]
    partial class Initial_Create
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BeerAppAPI.Models.Beer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("ABV");

                    b.Property<int>("BreweryId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("BreweryId");

                    b.ToTable("Beer");
                });

            modelBuilder.Entity("BeerAppAPI.Models.Brewery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Name");

                    b.Property<string>("Website");

                    b.HasKey("Id");

                    b.ToTable("Brewery");
                });

            modelBuilder.Entity("BeerAppAPI.Models.Beer", b =>
                {
                    b.HasOne("BeerAppAPI.Models.Brewery", "Brewery")
                        .WithMany("Beers")
                        .HasForeignKey("BreweryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
