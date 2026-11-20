using ChudoObuvApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ChudoObuvApp
{
    public partial class FormProducts : Form
    {
        public FormProducts()
        {
            InitializeComponent();
        }

        private void FormProducts_Load(object sender, EventArgs e)
        {
            
            LoadProducts();
        }

        private void LoadProducts()
        {
            FlowPanelProducts.Controls.Clear();

            DateTime TodayDate = DateTime.Today;
            DateTime FirstDayCurrentMonth = new DateTime(TodayDate.Year, TodayDate.Month, 1);
            DateTime FirstDayPrevMonth = FirstDayCurrentMonth.AddMonths(-1);

            DateOnly StartPrevMonth = DateOnly.FromDateTime(FirstDayPrevMonth);
            DateOnly EndPrevMonth = DateOnly.FromDateTime(FirstDayCurrentMonth.AddDays(-1));

            using Pr11Context Context = new Pr11Context();

            List<Product> ProductsList = Context.Products
                .Include(p => p.Category)
                .Include(p => p.Subcategory)
                .Include(p => p.ProductVariants)
                    .ThenInclude(v => v.OrderItems)
                        .ThenInclude(oi => oi.Order)
                .ToList();

            foreach (Product Item in ProductsList)
            {
                int TotalQuantity = Item.ProductVariants.Sum(v => v.StockQuantity);

                bool HasOrdersInPrevMonth = Item.ProductVariants
                    .SelectMany(v => v.OrderItems)
                    .Any(oi => oi.Order != null
                               && oi.Order.OrderDate >= StartPrevMonth
                               && oi.Order.OrderDate <= EndPrevMonth);

                bool HasDiscount = !HasOrdersInPrevMonth;

                Panel CardPanel = CreateProductCard(Item, TotalQuantity, HasDiscount);
                FlowPanelProducts.Controls.Add(CardPanel);
            }
        }

        private Panel CreateProductCard(Product Item, int TotalQuantity, bool HasDiscount)
        {
            Color CardBackColor = TotalQuantity <= 3
                ? Color.FromArgb(112, 178, 175)
                : Color.White;

            Panel Card = new Panel
            {
                Width = FlowPanelProducts.ClientSize.Width - 40,
                Height = 160,
                BackColor = CardBackColor,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10, 5, 10, 5)
            };

            PictureBox PicBox = new PictureBox
            {
                Width = 140,
                Height = 140,
                Location = new Point(10, 10),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            string ImagePath = GetProductImagePath(Item.ImageFile);
            if (File.Exists(ImagePath))
            {
                PicBox.Image = Image.FromFile(ImagePath);
            }

            string QuantityText = TotalQuantity > 5 ? "много" : "мало";
            string CategoryFull = Item.Subcategory != null
                ? $"{Item.Category?.Name} ({Item.Subcategory.Name})"
                : $"{Item.Category?.Name}";

            Label InfoLabel = new Label
            {
                Location = new Point(165, 10),
                Width = Card.Width - 360,
                Height = 140,
                Font = new Font("Calibri", 11F, FontStyle.Regular),
                Text = $"{Item.Manufacturer} | {Item.Name}\n" +
                       $"Категория: {CategoryFull}\n" +
                       $"Количество: {QuantityText}\n" +
                       $"Состав: {Item.Composition}"
            };

            Panel PricePanel = new Panel
            {
                Width = 180,
                Height = 140,
                Location = new Point(Card.Width - 190, 10),
                BackColor = Color.Transparent
            };

            if (HasDiscount)
            {
                decimal DiscountPrice = Item.BasePrice * 0.75m;

                Label OldPriceLabel = new Label
                {
                    Text = $"{Item.BasePrice:N2} руб.",
                    Font = new Font("Calibri", 11F, FontStyle.Strikeout),
                    ForeColor = Color.DarkRed,
                    Location = new Point(5, 35),
                    AutoSize = true
                };

                Label NewPriceLabel = new Label
                {
                    Text = $"{DiscountPrice:N2} руб. (-25%)",
                    Font = new Font("Calibri", 12F, FontStyle.Bold),
                    ForeColor = Color.Black,
                    Location = new Point(5, 65),
                    AutoSize = true
                };

                PricePanel.Controls.Add(OldPriceLabel);
                PricePanel.Controls.Add(NewPriceLabel);
            }
            else
            {
                Label RegularPriceLabel = new Label
                {
                    Text = $"{Item.BasePrice:N2} руб.",
                    Font = new Font("Calibri", 12F, FontStyle.Bold),
                    Location = new Point(5, 50),
                    AutoSize = true
                };

                PricePanel.Controls.Add(RegularPriceLabel);
            }

            Card.Controls.Add(PicBox);
            Card.Controls.Add(InfoLabel);
            Card.Controls.Add(PricePanel);

            return Card;
        }

        private string GetProductImagePath(string? ImageFileName)
        {
            string BaseDir = AppDomain.CurrentDomain.BaseDirectory;
            string ResDir = Path.Combine(BaseDir, "Resources");

            if (!string.IsNullOrWhiteSpace(ImageFileName) && Directory.Exists(ResDir))
            {
                string CleanName = Path.GetFileName(ImageFileName);
                string[] FoundFiles = Directory.GetFiles(ResDir, "*.*", SearchOption.AllDirectories);

                string? Match = FoundFiles.FirstOrDefault(f =>
                    Path.GetFileName(f).Equals(CleanName, StringComparison.OrdinalIgnoreCase) ||
                    Path.GetFileNameWithoutExtension(f).Equals(Path.GetFileNameWithoutExtension(CleanName), StringComparison.OrdinalIgnoreCase));

                if (Match != null)
                {
                    return Match;
                }
            }

            string Fallback = Path.Combine(ResDir, "picture.png");
            return File.Exists(Fallback) ? Fallback : string.Empty;
        }

    }
}
