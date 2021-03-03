﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Linq;
using System.Collections.ObjectModel;
using OfficeEquipmentManager.LocalDB;
using System.Windows.Shapes;
using System.Windows.Media;

namespace OfficeEquipmentManager
{
	/// <summary>
	/// Interaction logic for EquipmentListManagmentPage.xaml
	/// </summary>
	public partial class EquipmentListManagmentPage : Page
	{
		List<Equipment> equipment = ContextConnector.db.Equipment.ToList();

		public EquipmentListManagmentPage()
		{
			InitializeComponent();

			Update();
		}

		private void Update()
        {
			for (int i = 0; i < equipment.Count(); i++)
			{
				StackPanel panel = new StackPanel()
				{
					VerticalAlignment = VerticalAlignment.Center,
					HorizontalAlignment = HorizontalAlignment.Center
				};

				TextBlock textBlockName = new TextBlock
				{
					Text = "Название оргтехники: " + equipment[i].Name
				};

				TextBlock textBlockQuantity = new TextBlock
				{
					Text = "Количество"
				};

				TextBox textBoxQuantity = new TextBox
				{
					Text = equipment[i].Quantity.ToString(),
					Tag = equipment[i].Id
				};
				textBoxQuantity.TextChanged += TextBoxQuantity_TextChanged;

				//	BitmapImage bImage = new BitmapImage();
				//bImage.BeginInit();
				//bImage.UriSource = new Uri(equipment[i].ImagePathString);
				//bImage.EndInit();

				Image image = new Image();
				//image.Source = bImage;

				TextBlock textBoxSerial = new TextBlock
				{
					Text = "Серийный номер: " + equipment[i].SerialNumber.ToString()
				};

				TextBlock textBoxStatus = new TextBlock
				{
					Text = "Статус: " + equipment[i].EquipmentStatus.Status
				};

				TextBlock textBoxCharacteristic = new TextBlock
				{
					Text = "Характеристики: " + equipment[i].Сharacteristic
				};

				TextBlock textBoxCategory = new TextBlock
				{
					Text = "Категория: " + equipment[i].EquipmentCategory.Name
				};

				panel.Children.Add(textBlockName);
				panel.Children.Add(textBlockQuantity);
				panel.Children.Add(textBoxQuantity);
				panel.Children.Add(image);
				panel.Children.Add(textBoxSerial);
				panel.Children.Add(textBoxStatus);
				panel.Children.Add(textBoxCharacteristic);
				panel.Children.Add(textBoxCategory);

				StackPanel main = new StackPanel
				{
					VerticalAlignment = VerticalAlignment.Center,
					HorizontalAlignment = HorizontalAlignment.Center,
					Orientation = Orientation.Vertical
				};

				StackPanel BarcodePanel = new StackPanel
				{
					VerticalAlignment = VerticalAlignment.Center,
					HorizontalAlignment = HorizontalAlignment.Center,
					Orientation = Orientation.Horizontal
				};

				StackPanel stackNumbers = new StackPanel
				{
					Orientation = Orientation.Horizontal,
					VerticalAlignment = VerticalAlignment.Bottom,
					HorizontalAlignment = HorizontalAlignment.Center
				};

				char[] BarcodeNumbers = equipment[i].Barcode.BarcodeValue.ToString().ToArray();
				for (int k = 0; k < BarcodeNumbers.Length; k++)
				{
					Line BarcodeLine = new Line
					{
						X2 = 0,
						Y2 = 100,
						Stroke = Brushes.Black,
						StrokeThickness = int.Parse(BarcodeNumbers[k].ToString()) / 2,
						HorizontalAlignment = HorizontalAlignment.Stretch,
						VerticalAlignment = VerticalAlignment.Stretch,
						Margin = new Thickness(0, 0, 5, 0)
					};
					TextBlock number = new TextBlock
					{
						Text = BarcodeNumbers[k].ToString(),
						VerticalAlignment = VerticalAlignment.Bottom
					};
					BarcodePanel.Children.Add(BarcodeLine);
					stackNumbers.Children.Add(number);
				}
				main.Children.Add(BarcodePanel);
				main.Children.Add(stackNumbers);

				panel.Children.Add(main);
				ListBoxItem item = new ListBoxItem
				{
					Tag = equipment[i].Id,
					Content = panel
				};
				item.MouseDoubleClick += Item_MouseDoubleClick;
				listEquipment.Items.Add(item);
			}
		}

        private void Item_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
			int id = Convert.ToInt32(((ListBoxItem)sender).Tag);

			Equipment equipment = ContextConnector.db.Equipment.FirstOrDefault(x => x.Id == id);

			new MainResourses.EquipmentEditWindow(equipment).ShowDialog();
			listEquipment.Items.Clear();
			Update();
        }

        void ButtonEquipmentPlus_Click(object sender, RoutedEventArgs e)
		{
			int id = Convert.ToInt32((sender as Button).Tag);
			Equipment clickedEquipment = ContextConnector.db.Equipment.First(x => x.Id == id);
			clickedEquipment.Quantity += 1;
			ContextConnector.db.SaveChanges();
			listEquipment.ItemsSource = ContextConnector.db.Equipment.ToList();
		}
		void ButtonEquipmentMinus_Click(object sender, RoutedEventArgs e)
		{
			int id = Convert.ToInt32((sender as Button).Tag);
			Equipment clickedEquipment = ContextConnector.db.Equipment.First(x => x.Id == id);
			clickedEquipment.Quantity -= 1;
			ContextConnector.db.SaveChanges();
			listEquipment.ItemsSource = ContextConnector.db.Equipment.ToList();
		}
		void TextBoxQuantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			e.Handled = "1234567890".IndexOf(e.Text) < 0;
		}
		void TextBoxQuantity_TextChanged(object sender, TextChangedEventArgs e)
		{
			TextBox clickedTextBox = sender as TextBox;
			int id = Convert.ToInt32(clickedTextBox.Tag);
			Equipment clickedEquipment = ContextConnector.db.Equipment.First(x => x.Id == id);
			clickedEquipment.Quantity = int.Parse(clickedTextBox.Text);
			ContextConnector.db.SaveChanges();
			listEquipment.ItemsSource = ContextConnector.db.Equipment.ToList();
		}
		void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
			Frames.mainFrame.GoBack();
		}
    }
}