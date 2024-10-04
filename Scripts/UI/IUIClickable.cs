using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClanGenDotNet.Scripts.UI;

public interface IUIClickable
{
	void HandleElementInteraction();
	void ChangeTexture();
}