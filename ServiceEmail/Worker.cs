using EmailToSAPInvoice.ViewModels;

namespace ServiceEmail
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private Timer _timer;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            _timer = new Timer(OnTimerElapsed, null, TimeSpan.Zero, TimeSpan.FromMinutes(10));

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }

        private void OnTimerElapsed(object state)
        {
            var mainWindowViewModel = new MainWindowViewModel();
            mainWindowViewModel.GetDataInvoicePrueba();
        }
    }
}