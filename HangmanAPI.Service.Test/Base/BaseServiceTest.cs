
using HangmanAPI.Data.DataInit;

namespace HangmanAPI.Service.Test.Base {
    public class BaseServiceTest {
        protected IMapper mapper;
        protected IUnitOfWork unitOfWork;
        protected WordService wordService;
        protected GameService gameService;
        protected GuessService guessService;
        protected DbContextOptions<DataContext> dbContextOptions;
        protected DataContext dataContext;
        private DataInitializer dataInitializer;

        public BaseServiceTest() {

            dbContextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseSqlServer("Server=localhost;Database=HangmanAPI;User=sa;Password=H@ngM@n!219;")
                .Options;
            dataContext = new DataContext(dbContextOptions);

            var mappingConfig = new MapperConfiguration(mc => {
                mc.AddProfile(new GameProfile());
                mc.AddProfile(new GuessProfile());
                mc.AddProfile(new WordProfile());
            });

            mapper = mappingConfig.CreateMapper();

            unitOfWork = new UnitOfWork(dataContext);

            wordService = new WordService(unitOfWork, mapper);

            gameService = new GameService(unitOfWork, wordService, mapper);

            guessService = new GuessService(unitOfWork, mapper);

            dataInitializer = new DataInitializer(dataContext);

            Task.Run(() => dataInitializer.PopulateWordData()).Wait();
        }
    }
}
