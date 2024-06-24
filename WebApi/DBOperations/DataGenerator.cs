using Microsoft.EntityFrameworkCore;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if(context.Books.Any()) {
                    return;
                }
                context.Books.AddRange(
                    new Book
                    {
                         //Id = 1,
                         Title = "Kitap1",
                         GenreId = 1,
                         PageCount = 200,
                         PublishDate = new DateTime(2001, 6, 12)
                        },
                    new Book
                    {
                        //Id = 2,
                        Title = "Kitap2",
                        GenreId = 2,
                        PageCount = 400,
                        PublishDate = new DateTime(2005, 4, 11)
                    },
                    new Book
                    {
                        //Id = 3,
                        Title = "Kitap3",
                        GenreId = 2,
                        PageCount = 705,
                        PublishDate = new DateTime(2010, 9, 1)
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
