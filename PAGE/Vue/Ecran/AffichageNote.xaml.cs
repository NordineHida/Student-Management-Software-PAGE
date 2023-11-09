﻿using PAGE.APIEtudiant.Stockage;
using PAGE.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PAGE.Vue.Ecran
{
    /// <summary>
    /// Logique d'interaction pour AffichageNote.xaml
    /// </summary>
    public partial class AffichageNote : Window
    {
        private Note note;
        public AffichageNote(Note note)
        {
            InitializeComponent();
            this.note = note;   
            ChargerInfosNotes();
        }

        public void ChargerInfosNotes()
        {
            txtCategorie.Text = note.Categorie;
            txtNature.Text = note.Nature;
            txtCommentaire.Text = note.Commentaire;
        }

        /// <summary>
        /// Supprime une note via l'API
        /// </summary>
        /// <author>Laszlo</author>
        private void DeleteNote(object sender, RoutedEventArgs e)
        {
            EtuDAO.Instance.DeleteNote(note);
            this.Close();
        }
    }
}