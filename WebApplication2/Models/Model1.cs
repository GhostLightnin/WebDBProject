using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebApplication2.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=ModelIfNeeded")
        {
        }

        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Закупка> Закупка { get; set; }
        public virtual DbSet<Клиент> Клиент { get; set; }
        public virtual DbSet<Материал> Материал { get; set; }
        public virtual DbSet<Материал_объекта> Материал_объекта { get; set; }
        public virtual DbSet<Объект> Объект { get; set; }
        public virtual DbSet<Работа_на_объекте> Работа_на_объекте { get; set; }
        public virtual DbSet<Состав_закупки> Состав_закупки { get; set; }
        public virtual DbSet<Сотрудник> Сотрудник { get; set; }
        public virtual DbSet<Этап_выполнения> Этап_выполнения { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Закупка>()
                .HasMany(e => e.Состав_закупки)
                .WithRequired(e => e.Закупка)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Клиент>()
                .Property(e => e.Название_организации)
                .IsUnicode(false);

            modelBuilder.Entity<Клиент>()
                .Property(e => e.Город)
                .IsUnicode(false);

            modelBuilder.Entity<Клиент>()
                .Property(e => e.Адрес)
                .IsUnicode(false);

            modelBuilder.Entity<Клиент>()
                .Property(e => e.Телефон)
                .IsUnicode(false);

            modelBuilder.Entity<Клиент>()
                .Property(e => e.Почта)
                .IsUnicode(false);

            modelBuilder.Entity<Клиент>()
                .Property(e => e.Контактное_лицо)
                .IsUnicode(false);

            modelBuilder.Entity<Клиент>()
                .HasMany(e => e.Объект)
                .WithRequired(e => e.Клиент)
                .HasForeignKey(e => e.ID_заказчика)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Материал>()
                .Property(e => e.Наименование)
                .IsUnicode(false);

            modelBuilder.Entity<Материал>()
                .Property(e => e.Вид_товара)
                .IsUnicode(false);

            modelBuilder.Entity<Материал>()
                .Property(e => e.Единицы_измерения)
                .IsUnicode(false);

            modelBuilder.Entity<Материал>()
                .HasMany(e => e.Материал_объекта)
                .WithRequired(e => e.Материал)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Материал>()
                .HasMany(e => e.Состав_закупки)
                .WithRequired(e => e.Материал)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Материал_объекта>()
                .Property(e => e.Единицы_измерения)
                .IsUnicode(false);

            modelBuilder.Entity<Объект>()
                .Property(e => e.Наименование_заказа)
                .IsUnicode(false);

            modelBuilder.Entity<Объект>()
                .Property(e => e.Срок_выполнения_работ)
                .IsUnicode(false);

            modelBuilder.Entity<Объект>()
                .Property(e => e.Вид_работ)
                .IsUnicode(false);

            modelBuilder.Entity<Объект>()
                .Property(e => e.Статус_заказа)
                .IsUnicode(false);

            modelBuilder.Entity<Объект>()
                .HasMany(e => e.Материал_объекта)
                .WithRequired(e => e.Объект)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Объект>()
                .HasMany(e => e.Работа_на_объекте)
                .WithRequired(e => e.Объект)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Объект>()
                .HasMany(e => e.Этап_выполнения)
                .WithRequired(e => e.Объект)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Работа_на_объекте>()
                .Property(e => e.Выполняемая_работа)
                .IsUnicode(false);

            modelBuilder.Entity<Сотрудник>()
                .Property(e => e.ФИО)
                .IsUnicode(false);

            modelBuilder.Entity<Сотрудник>()
                .Property(e => e.Должность)
                .IsUnicode(false);

            modelBuilder.Entity<Сотрудник>()
                .Property(e => e.Телефон)
                .IsUnicode(false);

            modelBuilder.Entity<Сотрудник>()
                .HasMany(e => e.Работа_на_объекте)
                .WithRequired(e => e.Сотрудник)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Этап_выполнения>()
                .Property(e => e.Название_этапа)
                .IsUnicode(false);
        }
    }
}
