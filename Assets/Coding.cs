using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Text.RegularExpressions;
public class Coding : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI code;

	[SerializeField]
	private TextMeshProUGUI outputCode;

	[SerializeField]
	private Draggable led;


	string text;
	int pin;
	int tempPin;
	int delay;
	int tempDelay;
	
	public string lexer(string [] lines){
		if(lines.Length < 9){
        	return "Insufficient Number of Lines Error: Should be 9 lines excluding newlines";
        }
        else if(lines.Length > 9){
        	return "Exceed Max Number of  Lines Error : Should be 9 lines excluding newlines";
        }
        else{
        	string pattern = @"^voidsetup\(\){$";
        	string temp;
        	int length;
        	Match result = Regex.Match(lines[0].Replace(" ","").Trim(), pattern);
        	if(!result.Success){
        		return "Invalid Syntax: " + lines[0];
        	}
        	else{
        		pattern = @"^pinMode\((\d+),OUTPUT\);$";
        		temp = lines[1].Replace(" ","").Replace("\t","").Trim();
        		result = Regex.Match(temp, pattern);
        		if(!result.Success){
        			return "Invalid Syntax: " + lines[1];
        			
        		}
        		else{
        			length = temp.IndexOf(",") - temp.IndexOf("(") - 1 ;
        			pin = Int32.Parse(temp.Substring(temp.IndexOf("(") + 1, length ));
        			if( pin>= 2 && pin <= 13){
        				
        				pattern = @"^voidloop\(\){$";
        				result = Regex.Match(lines[3].Replace(" ","").Trim(), pattern);
        				if(!result.Success){
        					return "Invalid Syntax: " + lines[3];
        				}
        				else{
        					pattern = @"^digitalWrite\((\d+),HIGH\);$";
			        		temp = lines[4].Replace(" ","").Replace("\t","").Trim();
			        		result = Regex.Match(temp, pattern);
			        		if(!result.Success){
			        			return "Invalid Syntax: " + lines[4];
			        		}
			        		else{
			        			length = temp.IndexOf(",") - temp.IndexOf("(") - 1 ;
			        			tempPin = Int32.Parse(temp.Substring(temp.IndexOf("(") + 1, length ));
			        			if(tempPin != pin){
			        				return "digitalWrite pin number: " + tempPin + " does not match with pinMode pin number" ;
			        			}
			        			else{
			        				pattern = @"^delay\((\d+)\);$";
			        				temp = lines[5].Replace(" ","").Trim();
        							result = Regex.Match(temp, pattern);

        							if(!result.Success){
			        					return "Invalid Syntax: " + lines[5];
			        				}

        							else{
        								length = temp.IndexOf(")") - temp.IndexOf("(")-1;
        								delay = Int32.Parse(temp.Substring(temp.IndexOf("(") + 1, length ));
        								if(delay < 1000){
        									return "Invalid delay time: " + delay + " (Should be greater than 1000 since unit is in milliseconds";

        								}
        								else{
        									pattern = @"^digitalWrite\((\d+),LOW\);$";
							        		temp = lines[6].Replace(" ","").Replace("\t","").Trim();
							        		result = Regex.Match(temp, pattern);
							        		if(!result.Success){
							        			return "Invalid Syntax: " + lines[6];
							        		}
							        		else{
							        			length = temp.IndexOf(",") - temp.IndexOf("(") - 1 ;
							        			tempPin = Int32.Parse(temp.Substring(temp.IndexOf("(") + 1, length ));
							        			if(tempPin != pin){
							        				return "digitalWrite pin number: " + tempPin + " does not match with pinMode pin number" ;
							        			}
							        			else{
											        pattern = @"^delay\((\d+)\);$";
							        				temp = lines[7].Replace(" ","").Trim();
				        							result = Regex.Match(temp, pattern);

				        							if(!result.Success){
							        					return "Invalid Syntax: " + lines[5];
							        				}
							        				else{
							        					length = temp.IndexOf(")") - temp.IndexOf("(")-1;
        												tempDelay = Int32.Parse(temp.Substring(temp.IndexOf("(") + 1, length ));
        												if(tempDelay != delay){
        													return "delay time: " +tempDelay + " does not much with the previous one.";
        												}
        												else{
        													return "No errors found";
        												}

							        				}
							        			}

        									}
        								}
        								
        							}
			        			}

			        		}

        				}
        			}

        			else{
        				return "Invalid Pin Number: " + pin + "pin number should range from [2,13]";
        			}
        		}

        	}
        		
        }

	}

    public void compileCode(){
    	text = code.text;
    	string [] lines = {};
    	string [] split = text.Split('\n');
        foreach (string word in split){  
        	if(word.Length > 1){
        		int temp = lines.Length + 1;
        		Array.Resize(ref lines, temp);
        		lines[temp - 1] = word;
        	}
        }  
    	outputCode.text =  lexer(lines);      

    }

    public void implementCode(){
    	compileCode();
    	if(outputCode.text == "No errors found"){
    		outputCode.text = "Code Successfully uploaded";
    		led.setCodePin(pin);
    		led.setDelay(delay/1000f);
    		led.setHasCode(true);
    	}
    	else{
    		led.setHasCode(false);
    	}

    }
}
