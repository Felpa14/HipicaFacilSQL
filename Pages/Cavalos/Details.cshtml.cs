using System;
using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HipicaFacilSQL.Data;
using HipicaFacilSQL.Models;
using ZXing;
using ZXing.QrCode;
using System.IO;

namespace HipicaFacilSQL.Pages.Cavalos
{
    public class DetailsModel : PageModel
    {
        private readonly HipicaFacilSQL.Data.HipicaContext _context;

        public DetailsModel(HipicaFacilSQL.Data.HipicaContext context)
        {
            _context = context;
        }

        public Cavalo Cavalo { get; set; }
        public byte[] QRCodeBytes { get; private set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Cavalo = await _context.Cavalos.FirstOrDefaultAsync(m => m.ID == id);

            if (Cavalo == null)
            {
                return NotFound();
            }

            // Generate QR Code based on cavalo details
            GenerateQRCode(Cavalo);

            return Page();
        }

        private void GenerateQRCode(Cavalo cavalo)
        {
            // Concatenate cavalo details to encode in QR Code
            string textToEncode = $"Nome: {cavalo.Nome}\n" +
                $"Raça: {cavalo.Raca}\n" +
                $"Peso: {cavalo.Peso}\n" +
                $"Data de Nascimento: {cavalo.Idade}";



            var qrCodeWriter = new BarcodeWriterPixelData
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    CharacterSet = "UTF-8", // Definindo a codificação como UTF-8
                    Height = 300,
                    Width = 300
                }
            };

            var pixelData = qrCodeWriter.Write(textToEncode);

            using (var bitmap = new Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
            {
                for (int y = 0; y < pixelData.Height; y++)
                {
                    for (int x = 0; x < pixelData.Width; x++)
                    {
                        var color = Color.FromArgb(pixelData.Pixels[(y * pixelData.Width + x) * 4 + 3],
                                                   pixelData.Pixels[(y * pixelData.Width + x) * 4 + 2],
                                                   pixelData.Pixels[(y * pixelData.Width + x) * 4 + 1],
                                                   pixelData.Pixels[(y * pixelData.Width + x) * 4 + 0]);
                        bitmap.SetPixel(x, y, color);
                    }
                }

                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    QRCodeBytes = ms.ToArray();
                }
            }
        }
    }
}
