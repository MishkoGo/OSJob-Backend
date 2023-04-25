using Microsoft.EntityFrameworkCore;

namespace aspnetserver.Data
{
    internal sealed class AppDBContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Posts_action> Post_Action {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder) => dbContextOptionsBuilder.UseSqlite("Data Source=./Data/AppDB.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Post[] postsToSeed = new Post[6];
            Posts_action[] postsToSeed_2 = new Posts_action[6];

            for(int i = 1; i <= 6; i++)
            {
                //Заполняем посты
                postsToSeed[i - 1] = new Post
                {
                    //задаем новые свойства 
                    PostId = i,
                    Date = DateTime.Now,
                    From = $"From AI",
                    whom = $"Manager",
                    Text = $"This is task {i} and do it, please.",
                    Action = "",
                };
                postsToSeed_2[i - 1] = new Posts_action
                {
                    id_task = i,
                    Date = DateTime.Now,
                    whom = $"manager",
                    Text = $"Mi",
                };
            }

            //Говорим что бы EF заполнил это. Мы перейдем в postToseed
            //И это сообщит EF о том, что нужно заполнить 

            modelBuilder.Entity<Post>().HasData(postsToSeed);
            modelBuilder.Entity<Posts_action>().HasData(postsToSeed_2);
        }
    }
}
