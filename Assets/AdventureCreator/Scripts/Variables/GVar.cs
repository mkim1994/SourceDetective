/*
 *
 *	Adventure Creator
 *	by Chris Burton, 2013-2017
 *	
 *	"GVar.cs"
 * 
 *	This script is a data class for project-wide variables.
 * 
 */

using UnityEngine;

namespace AC
{

	/**
	 * A data class for global and local variables.
	 * Variables are created in the Variables Manager asset file, and copied to the RuntimeVariables component when the game begins.
	 */
	[System.Serializable]
	public class GVar
	{

		/** Its editor name. */
		public string label;
		/** Its internal ID number. */
		public int id;
		/** Its variable type. */
		public VariableType type;
		/** Its value, if an integer, popup or boolean. If a boolean, 0 = False, and 1 = True. */
		public int val;
		/** Its value, if a float. */
		public float floatVal;
		/** Its value, if a string. */
		public string textVal;
		/** An array of labels, if a popup. */
		public string[] popUps;
		/** Its value, if a Vector3 */
		public Vector3 vector3Val;
		/** What it links to.  A Variable can link to Options Data, or a PlayMaker Global Variable. */
		public VarLink link = VarLink.None;
		/** If linked to a PlayMaker Global Variable, the name of the PM variable. */
		public string pmVar;
		/** If True and linked to a PlayMaker Global Variable, then PM will be referred to for the initial value. */
		public bool updateLinkOnStart = false;
		/** True if the variable is currently selected in the Variable Manager. */
		public bool isEditing = false;
		/** If True, the variable's value can be translated (if PopUp or String) */
		public bool canTranslate = true;

		/** The translation ID number of the variable's string value (if type == VariableType.String), as generated by SpeechManager */
		public int textValLineID = -1;
		/** The translation ID number of the variables's PopUp values (if type == VariableType.PopUp), as generated by SpeechManager */
		public int popUpsLineID = -1;

		private float backupFloatVal;
		private int backupVal;

		private string[] runtimeTranslations;


		public GVar ()
		{}
		
		
		/**
		 * The main Constructor.
		 * An array of ID numbers is required, to ensure its own ID is unique.
		 */
		public GVar (int[] idArray)
		{
			val = 0;
			floatVal = 0f;
			textVal = "";
			type = VariableType.Boolean;
			id = 0;
			link = VarLink.None;
			pmVar = "";
			popUps = null;
			updateLinkOnStart = false;
			backupVal = 0;
			backupFloatVal = 0f;
			textValLineID = -1;
			popUpsLineID = -1;
			canTranslate = true;
			vector3Val = Vector3.zero;

			// Update id based on array
			foreach (int _id in idArray)
			{
				if (id == _id)
				{
					id ++;
				}
			}
			
			label = "Variable " + (id + 1).ToString ();
		}
		

		/**
		 * A Constructor that copies all values from another variable.
		 * This way ensures that no connection remains to the asset file.
		 */
		public GVar (GVar assetVar)
		{
			val = assetVar.val;
			floatVal = assetVar.floatVal;
			textVal = assetVar.textVal;
			type = assetVar.type;
			id = assetVar.id;
			label = assetVar.label;
			link = assetVar.link;
			pmVar = assetVar.pmVar;
			popUps = assetVar.popUps;
			updateLinkOnStart = assetVar.updateLinkOnStart;
			backupVal = assetVar.val;
			backupFloatVal = assetVar.floatVal;
			textValLineID = assetVar.textValLineID;
			popUpsLineID = assetVar.popUpsLineID;
			canTranslate = assetVar.canTranslate;
			vector3Val = assetVar.vector3Val;
		}
		
		
		/**
		 * Sets its value to that of its linked PlayMaker variable (if appropriate).
		 */
		public void Download ()
		{
			if (link == VarLink.PlaymakerGlobalVariable && pmVar != "")
			{
				if (!PlayMakerIntegration.IsDefinePresent ())
				{
					return;
				}
				
				if (type == VariableType.Integer || type == VariableType.PopUp)
				{
					SetValue (PlayMakerIntegration.GetGlobalInt (pmVar));
				}
				else if (type == VariableType.Boolean)
				{
					bool _val = PlayMakerIntegration.GetGlobalBool (pmVar);
					if (_val)
					{
						SetValue (1);
					}
					else
					{
						SetValue (0);
					}
				}
				else if (type == VariableType.String)
				{
					SetStringValue (PlayMakerIntegration.GetGlobalString (pmVar));
				}
				else if (type == VariableType.Float)
				{
					SetFloatValue (PlayMakerIntegration.GetGlobalFloat (pmVar));
				}
				else if (type == VariableType.Vector3)
				{
					SetVector3Value (PlayMakerIntegration.GetGlobalVector3 (pmVar));
				}
			}
		}
		
		
		/**
		 * Sets the value of its linked Options Data or PlayMaker variable to its value (if appropriate).
		 */
		public void Upload ()
		{
			if (link == VarLink.PlaymakerGlobalVariable && pmVar != "")
			{
				if (!PlayMakerIntegration.IsDefinePresent ())
				{
					return;
				}
				
				if (type == VariableType.Integer || type == VariableType.PopUp)
				{
					PlayMakerIntegration.SetGlobalInt (pmVar, val);
				}
				else if (type == VariableType.Boolean)
				{
					if (val == 1)
					{
						PlayMakerIntegration.SetGlobalBool (pmVar, true);
					}
					else
					{
						PlayMakerIntegration.SetGlobalBool (pmVar, false);
					}
				}
				else if (type == VariableType.String)
				{
					PlayMakerIntegration.SetGlobalString (pmVar, textVal);
				}
				else if (type == VariableType.Float)
				{
					PlayMakerIntegration.SetGlobalFloat (pmVar, floatVal);
				}
				else if (type == VariableType.Vector3)
				{
					PlayMakerIntegration.SetGlobalVector3 (pmVar, vector3Val);
				}
			}
			else if (link == VarLink.OptionsData)
			{
				Options.SavePrefs ();
			}
		}
		

		/**
		 * Backs up its value.
		 * Necessary when skipping ActionLists that involve checking variable values.
		 */
		public void BackupValue ()
		{
			backupVal = val;
			backupFloatVal = floatVal;
		}
		
		
		/**
		 * Restores its value from backup. 
		 * Necessary when skipping ActionLists that involve checking variable values.
		 */
		public void RestoreBackupValue ()
		{
			val = backupVal;
			floatVal = backupFloatVal;
		}
		
		
		/**
		 * Sets the value if its type is String.
		 */
		public void SetStringValue (string newValue)
		{
			string originalValue = textVal;

			textVal = newValue;

			if (originalValue != textVal)
			{
				KickStarter.eventManager.Call_OnVariableChange (this);
			}
		}
		
		
		/**
		 * <summary>Sets the value if its type is Float.</summary>
		 * <param name = "newValue">The new float value</param>
		 * <param name = "setVarMethod">How the new value affects the old (replaces, increases by, or randomises)</param>
		 */
		public void SetFloatValue (float newValue, SetVarMethod setVarMethod = SetVarMethod.SetValue)
		{
			float originalValue = floatVal;

			if (setVarMethod == SetVarMethod.IncreaseByValue)
			{
				floatVal += newValue;
			}
			else if (setVarMethod == SetVarMethod.SetAsRandom)
			{
				floatVal = Random.Range (0f, newValue);
			}
			else
			{
				floatVal = newValue;
			}

			if (originalValue != floatVal)
			{
				KickStarter.eventManager.Call_OnVariableChange (this);
			}
		}


		/**
		 * <summary>Sets the value if its type is Vector3.</summary>
		 * <param name = "newValue">The new Vector3 value</param>
		 */
		public void SetVector3Value (Vector3 newValue)
		{
			Vector3 originalValue = vector3Val;
			vector3Val = newValue;

			if (originalValue != newValue)
			{
				KickStarter.eventManager.Call_OnVariableChange (this);
			}
		}
		

		/**
		 * <summary>Sets the value if its type is Integer, Boolean or PopUp.</summary>
		 * <param name = "newValue">The new integer value</param>
		 * <param name = "setVarMethod">How the new value affects the old (replaces, increases by, or randomises)</param>
		 */
		public void SetValue (int newValue, SetVarMethod setVarMethod = SetVarMethod.SetValue)
		{
			int originalValue = val;

			if (setVarMethod == SetVarMethod.IncreaseByValue)
			{
				val += newValue;
			}
			else if (setVarMethod == SetVarMethod.SetAsRandom)
			{
				val = Random.Range (0, newValue);
			}
			else
			{
				val = newValue;
			}
			
			if (type == VariableType.Boolean)
			{
				if (val > 0)
				{
					val = 1;
				}
				else
				{
					val = 0;
				}
			}
			else if (type == VariableType.PopUp)
			{
				if (val < 0)
				{
					val = 0;
				}
				else if (val >= popUps.Length)
				{
					val = popUps.Length - 1;
				}
			}

			if (originalValue != val)
			{
				KickStarter.eventManager.Call_OnVariableChange (this);
			}
		}


		/**
		 * <summary>Transfers translation data from RuntimeLanguages to the variable itself. This allows it to be transferred to other variables with the 'Variable: Copy' Action.</summary>
		 */
		public void CreateRuntimeTranslations ()
		{
			runtimeTranslations = null;
			if (HasTranslations ())
			{
				if (type == VariableType.String)
				{
					runtimeTranslations = KickStarter.runtimeLanguages.GetTranslations (textValLineID);
				}
				else if (type == VariableType.PopUp)
				{
					runtimeTranslations = KickStarter.runtimeLanguages.GetTranslations (popUpsLineID);
				}
			}
		}


		/**
		 * <summary>Gets the variable's translations, if they exist.</summary>
		 * <returns>The variable's translations, if they exist, as an array.</summary>
		 */
		public string[] GetTranslations ()
		{
			return runtimeTranslations;
		}


		/**
		 * <summary>Copies the value of another variable onto itself.</summary>
		 * <param name = "oldVar">The variable to copy from</param>
		 * <param name = "oldLocation">The location of the variable to copy (Global, Local)</param>
		 */
		public void CopyFromVariable (GVar oldVar, VariableLocation oldLocation)
		{
			if (oldLocation == VariableLocation.Global)
			{
				oldVar.Download ();
			}

			if (type == VariableType.Integer || type == VariableType.Boolean || type == VariableType.PopUp)
			{
				int oldValue = oldVar.val;

				if (oldVar.type == VariableType.Float)
				{
					oldValue = (int) oldVar.floatVal;
				}
				else if (oldVar.type == VariableType.String)
				{
					float oldValueAsFloat = 0f;
					float.TryParse (oldVar.textVal, out oldValueAsFloat);
					oldValue = (int) oldValueAsFloat;
				}

				if (type == VariableType.PopUp && oldVar.HasTranslations ())
				{
					runtimeTranslations = oldVar.GetTranslations ();
				}
				else
				{
					runtimeTranslations = null;
				}

				SetValue (oldValue, SetVarMethod.SetValue);
			}
			else if (type == VariableType.Float)
			{
				float oldValue = oldVar.floatVal;

				if (oldVar.type == VariableType.Integer || oldVar.type == VariableType.Boolean || oldVar.type == VariableType.PopUp)
				{
					oldValue = (float) oldVar.val;
				}
				else if (oldVar.type == VariableType.String)
				{
					float.TryParse (oldVar.textVal, out oldValue);
				}

				SetFloatValue (oldValue, AC.SetVarMethod.SetValue);
			}
			else if (type == VariableType.String)
			{
				string oldValue = oldVar.GetValue ();
				textVal = oldValue;

				if (oldVar.HasTranslations ())
				{
					runtimeTranslations = oldVar.GetTranslations ();
				}
				else
				{
					runtimeTranslations = null;
				}
			}
			else if (type == VariableType.Vector3)
			{
				Vector3 oldValue = oldVar.vector3Val;
				vector3Val = oldValue;
			}
		}
		
		
		/**
		 * <summary>Returns the variable's value.</summary>
		 * <returns>The value, as a formatted string.</returns>
		 */
		public string GetValue (int languageNumber = 0)
		{
			if (!canTranslate)
			{
				languageNumber = 0;
			}

			if (type == VariableType.Integer)
			{
				return val.ToString ();
			}
			else if (type == VariableType.PopUp)
			{
				if (languageNumber > 0)
				{
					if (runtimeTranslations != null && runtimeTranslations.Length >= languageNumber)
					{
						string popUpsString = runtimeTranslations[languageNumber-1];
						string[] popUpsNew = popUpsString.Split ("]"[0]);
						if (val >= 0 && val < popUpsNew.Length)
						{
							return popUpsNew[val];
						}
					}
				}

				if (popUps == null || popUps.Length == 0) return "";
				val = Mathf.Max (0, val);
				val = Mathf.Min (val, popUps.Length-1);
				return popUps [val];
			}
			else if (type == VariableType.String)
			{
				if (languageNumber > 0)
				{
					if (runtimeTranslations != null && runtimeTranslations.Length >= languageNumber)
					{
						return runtimeTranslations[languageNumber-1];
					}
				}
				return textVal;
			}
			else if (type == VariableType.Float)
			{
				return floatVal.ToString ();
			}
			else if (type == VariableType.Vector3)
			{
				return "(" + vector3Val.x.ToString () + ", " + vector3Val.y.ToString () + ", " + vector3Val.z.ToString () + ")";
			}
			else
			{
				if (val == 0)
				{
					return "False";
				}
				else
				{
					return "True";
				}
			}
		}


		/**
		 * <summary>Gets all possible PopUp values as a single string, where the values are separated by a ']' character.</summary>
		 * <returns>All possible PopUp values as a single string, where the values are separated by a ']' character.</returns>
		 */
		public string GetPopUpsString ()
		{
			string result = "";
			foreach (string popUp in popUps)
			{
				result += popUp + "]";
			}
			if (result.Length > 0)
			{
				return result.Substring (0, result.Length-1);
			}
			return "";
		}


		/**
		 * <summary>Checks if the Variable is translatable.</summary>
		 * <returns>True if the Variable is translatable</returns>
		 */
		public bool HasTranslations ()
		{
			if (type == VariableType.String || type == VariableType.PopUp)
			{
				return canTranslate;
			}
			return false;
		}


		/** Its value, if an integer. */
		public int IntegerValue
		{
			get
			{
				return val;
			}
			set
			{
				val = value;
			}
		}


		/** Its value, if a boolean. */
		public bool BooleanValue
		{
			get
			{
				return (val == 1);
			}
			set
			{
				val = (value) ? 1 : 0;
			}
		}


		/** Its value, if a float. */
		public float FloatValue
		{
			get
			{
				return floatVal;
			}
			set
			{
				floatVal = value;
			}
		}


		/** Its value, if a string. */
		public string TextValue
		{
			get
			{
				return textVal;
			}
			set
			{
				textVal = value;
			}
		}


		/** Its value, if a Vector3. */
		public Vector3 Vector3Value
		{
			get
			{
				return vector3Val;
			}
			set
			{

				vector3Val = value;
			}
		}
		
	}

}