namespace EFCoreORM
{
    public static class EFBasics
    {
        public static void Run()
        {
            ReadData();
            //ReadOneItem();
            //InsertData();
            //UpdateData();
            //DeleteData();
            //ExecuteTransaction();

        }

        public static void ReadData()
        {
            // Create a new DbContext.
            using var context = new AppDbContext();
            // Get all wallets.
            var wallets = context.Wallets.ToList();
            // Display the wallets.
            foreach (var wallet in wallets)
                Console.WriteLine(wallet);
        }
        public static void ReadOneItem()
        {
            // Create a new DbContext.
            using var context = new AppDbContext();
            var id = 1;
            // Get the wallet.
            var wallet = context.Wallets.Single(w => w.Id == id);
            // Display the wallets.
            Console.WriteLine(wallet);
        }
        public static void InsertData()
        {
            // Create a new DbContext.
            using var context = new AppDbContext();
            // Create a new wallet.
            var wallet = new Wallet
            {
                Holder = "name",
                Balance = 243
            };
            // Add the wallet to the context.
            context.Wallets.Add(wallet);
            // Save the changes.
            context.SaveChanges();
        }
        public static void UpdateData()
        {
            // Create a new DbContext.
            using var context = new AppDbContext();
            // Get the wallet by its ID.
            // Finds an entity with the given primary key values.
            var wallet = context.Wallets.Find(10);
            // Update the wallet balance.
            wallet?.Balance = 500;
            Console.WriteLine(wallet);
            // Save the changes.
            context.SaveChanges();
        }
        public static void DeleteData()
        {
            // Create a new DbContext.
            using var context = new AppDbContext();
            // Get the wallet by its ID.
            var wallet = context.Wallets.Find(16);
            if (wallet != null)
            {
                // Delete the wallet.
                context.Wallets.Remove(wallet);
                // Save the changes.
                context.SaveChanges();
            }
        }
        public static void ExecuteTransaction()
        {
            // Create a new DbContext.
            using var context = new AppDbContext();
            // Begin a database transaction.
            using var transaction = context.Database.BeginTransaction();

            const int idFrom = 2;
            const int idTo = 1;
            const int amountToTransfer = 1000;

            // Get the source wallet.
            var walletFrom = context.Wallets.Find(idFrom);

            // Get the destination wallet.
            var walletTo = context.Wallets.Find(idTo);

            // Check if the wallets are found.
            if (walletFrom == null || walletTo == null)
            {
                Console.WriteLine("Wallets not found.");
                return;
            }

            // Subtract the amount from the source wallet.
            walletFrom.Balance -= amountToTransfer;
            context.SaveChanges();

            // Add the amount to the destination wallet.
            walletTo.Balance += amountToTransfer;
            context.SaveChanges();

            // Update the source wallet.
            context.Wallets.Update(walletFrom);

            // Update the destination wallet.
            context.Wallets.Update(walletTo);

            // Commit both changes as one transaction.
            transaction.Commit();
        }
    }
}