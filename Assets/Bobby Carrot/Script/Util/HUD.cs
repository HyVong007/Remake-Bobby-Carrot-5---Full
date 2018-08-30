using UnityEngine;
using UnityEngine.UI;
using BobbyCarrot.Platforms;


namespace BobbyCarrot.Util
{
	public class HUD : MonoBehaviour
	{
		[SerializeField] private Text carrotCount, eggCount;
		[SerializeField] private ItemName_Text_Dict itemDict;

		public static HUD instance { get; private set; }


		private void Awake()
		{
			if (!instance) instance = this;
			else if (this != instance)
			{
				Destroy(gameObject);
				return;
			}

			Board.onStart += () =>
			{
				if (Carrot.countDown > 0)
				{
					carrotCount.gameObject.SetActive(true);
					carrotCount.text = Carrot.countDown.ToString();
				}

				if (EasterEgg.countDown > 0)
				{
					eggCount.gameObject.SetActive(true);
					eggCount.text = EasterEgg.countDown.ToString();
				}
			};

			Carrot.onRemoveCarrot += () => { carrotCount.text = Carrot.countDown.ToString(); };
			EasterEgg.onAddEgg += () => { eggCount.text = EasterEgg.countDown.ToString(); };
		}


		public void SetItemUI(Item.Name name)
		{
			int count = Item.count[name];
			var ui = itemDict[name];
			if (count <= 0) ui.gameObject.SetActive(false);
			else
			{
				ui.text = count.ToString();
				ui.gameObject.SetActive(true);
			}
		}
	}
}
