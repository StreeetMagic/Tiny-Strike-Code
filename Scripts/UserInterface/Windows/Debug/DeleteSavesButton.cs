using Buttons;
using UnityEngine;

public class DeleteSavesButton : BaseButton
{
  private void Start()
  {
    Button.onClick.AddListener(DeleteSaves);
  }

  private void DeleteSaves()
  {
    PlayerPrefs.DeleteAll();
  }
}