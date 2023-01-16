using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CardGame.Cards.Model;

namespace CardGame.Cards
{
    public class UICard : MonoBehaviour
    {

        


        [Header("Общее")]
        [SerializeField] private Image _image;
        [SerializeField] private Image _imagePrice;
        [SerializeField] private Image _imageClosed;
        [SerializeField] private Text _nameText;
        [SerializeField] private Text _descriptionText;
        [SerializeField] private Text _priceText;

        [Header("Юнит")]
        [SerializeField] private Text _damageText;
        [SerializeField] private Text _healthText;
        [SerializeField] private Text _classText;
        [SerializeField] private GameObject _imageHealth;
        [SerializeField] private GameObject _imageDamage;
        [SerializeField] private GameObject _defendStatus;
        [SerializeField] private GameObject _attackStatus;


        [SerializeField] private GameObject _LineAttacActive;
       


        public void Setup(ModelCard model) 
        {
            
          
            _image.sprite = model.SpriteCard;
            _descriptionText.text = model.Description;
            _nameText.text = model.Name;
            _priceText.text = model.Price.ToString();
          
        }

        public void OpenCard(ModelCard model) 
        {
            _imagePrice.gameObject.SetActive(true);
            _imageClosed.gameObject.SetActive(false);
            if (model.TypeCard == TypeCard.Unit)
            {
                var modelUnit = model as ModelUnit;
                _classText.text = modelUnit.Class.ToString();
                _healthText.text = modelUnit.Health.ToString();
                _damageText.text = modelUnit.Damage.ToString();
                _imageHealth.SetActive(true);
                _imageDamage.SetActive(true);

                //Это тип нужно
                /*unitCard.helthChanged.AddListener(health => _healthText.text = health.ToString());     
                unitCard.statusChanged.AddListener(status =>
                {
                    switch (status)
                    {
                        case Status.Attacker:
                            OnAttackStatus();
                            break;
                        case Status.Defender:
                            OnDefendStatus();
                            break;
                        default:
                            OnAnotherStatus();
                            break;
                    }
                });
                */
            }
            if (model.TypeCard == TypeCard.Effect)
            {
                _imageHealth.SetActive(false);
                _imageDamage.SetActive(false);
            }
        }

        public void CloseCard() 
        {
            _imageClosed.gameObject.SetActive(true);
            _imagePrice.gameObject.SetActive(false);
            _imageHealth.SetActive(false);
            _imageDamage.SetActive(false);
        }

        public void ActivCard() 
        {
            _LineAttacActive.SetActive(true);
        }

        public void DeactivCard() 
        {
            _LineAttacActive.SetActive(false);
        }


        public void UpdateHealth(int health)
        {
            _healthText.text = health.ToString();
        }


        public void UpdateDamage(int damage) 
        {
            _damageText.text = damage.ToString();
        }



        public void OnAttackStatus()
        {
            _attackStatus.SetActive(true);
        }

        public void OnDefendStatus()
        {
            _defendStatus.SetActive(true);
        }

        public void OnAnotherStatus()
        {
            _attackStatus.SetActive(false);
            _defendStatus.SetActive(false);
        }
    }
}