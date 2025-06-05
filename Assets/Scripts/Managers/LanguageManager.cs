using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager Instance;
    
    [Header("Language Settings")]
    public string currentLanguage = "English";
    
    [Header("Debug Settings")]
    public bool enableDebugLogs = true;
    public bool showRegisteredTexts = true;
    
    // Dictionary to store all translations
    private Dictionary<string, Dictionary<string, string>> translations;
    
    // Dictionary to store dialogue translations
    private Dictionary<string, Dictionary<string, DialogueTranslation>> dialogueTranslations;
    
    // List to keep track of all text components that need updating
    private List<LocalizedText> localizedTexts = new List<LocalizedText>();
    
    // List to keep track of all localized dialogue components
    private List<LocalizedDialogue> localizedDialogues = new List<LocalizedDialogue>();
    
    void Awake()
    {
        // Singleton pattern - only one LanguageManager can exist
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeTranslations();
            InitializeDialogueTranslations();
            LoadSavedLanguage();
            
            if (enableDebugLogs)
                Debug.Log($"LanguageManager initialized with language: {currentLanguage}");
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void InitializeTranslations()
    {
        translations = new Dictionary<string, Dictionary<string, string>>();
        
        // English translations
        translations["English"] = new Dictionary<string, string>
        {
            {"options", "Options"},
            {"language", "Language"},
            {"spanish", "Spanish"},
            {"french", "French"},
            {"english", "English"},
            {"camera", "Camera"},
            {"audio", "Audio"},
            {"controls", "Controls"},
            {"instructions", "W,A,S,D to move, E to interact\nRun in a circle to create an ink block!\nLshift to sprint\nSpacebar to jump\n\n\nAccessibility Mode: \nLclick to Move, Rclick to Jump \nPush scroll button to interact\nForward button to open pause menu\nBack button to sprint"},
            {"acc_mode", "Accessibility Mode"},
            {"music", "Music"},
            {"back", "Back"},
            {"resume", "Resume"},
            {"start", "Start"},
            {"exit", "Quit"},
            {"quit", "Quit"},
            {"save", "Save"},
            {"load", "Load"},
            {"pause", "Pause"},
            {"well", "Wishing Well"},
            {"ink", "Ink Block"},
            {"esc", "Press 'esc'  or 'forward button' on mouse for pause menu"},
            {"x_sensitivity", "X Sensitivity"},
            {"y_sensitivity", "Y Sensitivity"},
            {"field_of_view", "Field of View"},
            {"music_volume", "Music Volume"},
            {"invert_x", "Invert X"},
            {"invert_y", "Invert Y"}
        };
        
        // Spanish translations
        translations["Spanish"] = new Dictionary<string, string>
        {
            {"options", "Opciones"},
            {"language", "Idioma"},
            {"spanish", "Español"},
            {"french", "Francés"},
            {"english", "Inglés"},
            {"camera", "Cámara"},
            {"audio", "Audio"},
            {"controls", "Controles"},
            {"instructions", "W, A, S, D para moverte, E para interactuar.\n¡Corre en círculo para crear un bloque de tinta!\nShift izquierdo para correr.\nBarra espaciadora para saltar.\n\nModo de accesibilidad:\nClic izquierdo para moverte, clic derecho para saltar.\nPresiona el botón de desplazamiento para interactuar.\nAvanzar para abrir el menú de pausa.\nAtrás para correr."},
            {"acc_mode", "Modo de accesibilidad"},
            {"music", "Música"},
            {"back", "Atrás"},
            {"resume", "Continuar"},
            {"start", "Empezar"},
            {"exit", "Salir"},
            {"quit", "Salir"},
            {"save", "Guardar"},
            {"load", "Cargar"},
            {"pause", "Pausa"},
            {"well", "Deseando Bien"},
            {"ink", "Bloque de tinta"},
            {"esc", "Presione 'esc' o 'botón de avance' en el mouse para el menú de pausa"},
            {"x_sensitivity", "Sensibilidad X"},
            {"y_sensitivity", "Sensibilidad Y"},
            {"field_of_view", "Campo de Visión"},
            {"music_volume", "Volumen de Música"},
            {"invert_x", "Invertir X"},
            {"invert_y", "Invertir Y"}
        };
        
        // French translations
        translations["French"] = new Dictionary<string, string>
        {
            {"options", "Options"},
            {"language", "Langue"},
            {"spanish", "Espagnol"},
            {"french", "Français"},
            {"english", "Anglais"},
            {"camera", "Caméra"},
            {"audio", "Audio"},
            {"controls", "Contrôles"},
            {"instructions", "W, A, S, D pour se déplacer, E pour interagir\nCourez en cercle pour créer un bloc dencre!\nMaj gauche pour sprinter\nBarre despace pour sauter\nMode daccessibilité:\nClic gauche pour se déplacer, clic droit pour sauter\nAppuyez sur le bouton de défilement pour interagir\nBouton Suivant pour ouvrir le menu Pause\nBouton Retour pour sprinter"},
            {"acc_mode", "Mode d'accessibilité"},
            {"music", "Musique"},            
            {"back", "Retour"},
            {"resume", "Reprendre"},
            {"start", "Commencer"},
            {"exit", "Quitter"},
            {"quit", "Quitter"},
            {"save", "Sauvegarder"},
            {"load", "Charger"},
            {"pause", "Pause"},
            {"well", "Souhaitant Bien"},
            {"ink", "Bloc d'encre"},
            {"esc", "Appuyez sur « esc » ou sur le bouton « avance » de la souris pour le menu de pause"},
            {"x_sensitivity", "Sensibilité X"},
            {"y_sensitivity", "Sensibilité Y"},
            {"field_of_view", "Champ de Vision"},
            {"music_volume", "Volume Musique"},
            {"invert_x", "Inverser X"},
            {"invert_y", "Inverser Y"}
        };
        
        if (enableDebugLogs)
        {
            Debug.Log($"LanguageManager: Loaded {translations.Count} languages");
            foreach (var lang in translations.Keys)
            {
                Debug.Log($"  - {lang}: {translations[lang].Count} translations");
            }
        }
    }
    
    void InitializeDialogueTranslations()
    {
        dialogueTranslations = new Dictionary<string, Dictionary<string, DialogueTranslation>>();
        
        // Initialize dialogue translations for each language
        dialogueTranslations["English"] = new Dictionary<string, DialogueTranslation>();
        dialogueTranslations["Spanish"] = new Dictionary<string, DialogueTranslation>();
        dialogueTranslations["French"] = new Dictionary<string, DialogueTranslation>();
        
        // Example dialogue entries - you can add more here
        
        // Jeff dialogue
        dialogueTranslations["English"]["jeff"] = new DialogueTranslation
        {
            name = "Jeff",
            sentences = new string[]
            {
                "Freakin' Frijoles! An Inkeeper! How is this even possible!? I thought they had gone extinct!",
                "Oh pardon my manners, my name is Jeff! I'm a resident here. My life got uprooted after the drowning, but I'm making ends meet!",
                "If you ever want to know more about this world, let me know!"
            }
        };
        dialogueTranslations["English"]["joe"] = new DialogueTranslation
        {
            name = "Joe",
            sentences = new string[]
            {
                "....?",
                "You've seen me before?",
                "Ohhh you must mean my twin brother Jeff!",
                "Yeah I hate that guy. We had a falling out a couple of weeks ago, so I moved out."
            }
        };
        dialogueTranslations["English"]["jay"] = new DialogueTranslation
        {
            name = "Jay",
            sentences = new string[]
            {
                "...",
                "I come here every year to mourn my wife....",
                "I live in the village to the left of here....",
                "It hasn't been easy, but it's getting better bit by bit...."
            }
        };
        dialogueTranslations["English"]["villager1"] = new DialogueTranslation
        {
            name = "Villager 1",
            sentences = new string[]
            {
                "If you walk to the right of here, we have a cemetery to mourn some of the dead village elders from here. ",
                "We've never had an inkeeper live in this village though. They say that if an inkeeper dies, black ink starts floating in the place that they died. ",
                "Legend has it that an inkeeper died here, we had a wishing well built in the exact place where they died. "
            }
        };
        dialogueTranslations["English"]["villager2"] = new DialogueTranslation
        {
            name = "Villager 2",
            sentences = new string[]
            {
                "The Inkeepers had a leader they called their king.",
                "But one day as the inkeepers were dying the king was nowhere to be found. ",
                "Many have searched for him, but all have failed."
            }
        };
        dialogueTranslations["English"]["villager3"] = new DialogueTranslation
        {
            name = "Villager 2",
            sentences = new string[]
            {
                "...",
                "I called my teacher 'mom' today.... ",
                "I can never recover from this level of embarassment.....",
                "All the kids were laughing at me...."
            }
        };
        dialogueTranslations["English"]["villager4"] = new DialogueTranslation
        {
            name = "Villager 1",
            sentences = new string[]
            {
                "Legend has it the inkeepers built hidden structures that are buried here....",
                "Archaelogists have tried searching for them, but no one has found anything...",
                "I wonder where they could be....",
                "Maybe they need some sort of key...? Like a book...?"
            }
        };
        dialogueTranslations["English"]["villager5"] = new DialogueTranslation
        {
            name = "Villager 3",
            sentences = new string[]
            {
                "Apparently if you press 'E' twice next to this fountain, you can save your progress.",
                "What's that? How do I know? ",
                "...",
                "I don't know man try it for yourself..."
            }
        };
        dialogueTranslations["English"]["villager6"] = new DialogueTranslation
        {
            name = "Villager 0",
            sentences = new string[]
            {
                "....",
                "This giant pillar right here is the grave of the king...   ",
                "No one was able to find him during the inkeepers' time of need... Many believe that he is dead... So they built this tomb in his honor...",
                "Me personally? I think he is dead too... The king loved his people... I refuse to believe that he'd willingly leave us when we needed him most... "
            }
        };
        dialogueTranslations["English"]["villager7"] = new DialogueTranslation
        {
            name = "Villager 1",
            sentences = new string[]
            {
                "Inkeepers used to live in this village...",
                "But they are all extinct....",
                "Now it's just me....",
                "I'm very lonely... Sometimes I take rocks, dress them up, and draw faces on them... They're my only friends now...  "
            }
        };



        dialogueTranslations["Spanish"]["jeff"] = new DialogueTranslation
        {
            name = "Jeff",
            sentences = new string[]
            {
                "¡Frijoles malditos! ¡Un Posadero! ¡¿Cómo es esto posible?! ¡Pensé que se habían extinguido!",
                "Oh, perdona mis modales, ¡mi nombre es Jeff! Soy residente aquí. Mi vida cambió después del ahogamiento, ¡pero estoy saliendo adelante!",
                "¡Si alguna vez quieres saber más sobre este mundo, házmelo saber!"
            }
        };
        dialogueTranslations["Spanish"]["joe"] = new DialogueTranslation
        {
            name = "Joe",
            sentences = new string[]
            {
                "....?",
                "¿Me has visto antes?",
                "¡Ohhh, debes referirte a mi hermano gemelo Jeff!",
                "Sí, odio a ese tipo. Tuvimos una pelea hace un par de semanas, así que me mudé."
            }
        };
        dialogueTranslations["Spanish"]["jay"] = new DialogueTranslation
        {
            name = "Jay",
            sentences = new string[]
            {
                "...",
                "Vengo aquí todos los años a llorar a mi esposa...",
                "Vivo en el pueblo a la izquierda de aquí...",
                "No ha sido fácil, pero va mejorando poco a poco..."
            }
        };        
        dialogueTranslations["Spanish"]["villager1"] = new DialogueTranslation
        {
            name = "Villager 1",
            sentences = new string[]
            {
                "Si caminas a la derecha de aquí, tenemos un cementerio para recordar a algunos de los ancianos fallecidos de la aldea.",

                "Aunque nunca hemos tenido un posadero viviendo en este pueblo. Dicen que si un posadero muere, la tinta negra empieza a flotar en el lugar donde murió.",

                "Cuenta la leyenda que un posadero murió aquí; hicimos construir un pozo de los deseos en el mismo lugar donde murió."
            }
        };
        dialogueTranslations["Spanish"]["villager2"] = new DialogueTranslation
        {
            name = "Villager 2",
            sentences = new string[]
            {
                "Los guardianes tenían un líder al que llamaban su rey.",

                "Pero un día, mientras los guardianes morían, el rey desapareció.",

                "Muchos lo han buscado, pero todos han fracasado."
            }
        };
        dialogueTranslations["Spanish"]["villager3"] = new DialogueTranslation
        {
            name = "Villager 2",
            sentences = new string[]
            {
                "...", 
                "Hoy llamé 'mamá' a mi maestra...", 
                "Nunca podré recuperarme de este nivel de vergüenza...", 
                "Todos los niños se reían de mí..."
            }
        };
        dialogueTranslations["Spanish"]["villager4"] = new DialogueTranslation
        {
            name = "Villager 1",
            sentences = new string[]
            {
                "Cuenta la leyenda que los guardianes construyeron estructuras ocultas que están enterradas aquí...", "Los arqueólogos han intentado buscarlas, pero nadie ha encontrado nada...", "Me pregunto dónde estarán...", "Quizás necesiten algún tipo de llave... ¿Algo así como un libro...?"
            }
        };
        dialogueTranslations["Spanish"]["villager5"] = new DialogueTranslation
        {
            name = "Villager 3",
            sentences = new string[]
            {
                "Parece que si pulsas 'E' dos veces junto a esta fuente, puedes guardar tu progreso.", "¿Qué es eso? ¿Cómo lo sé?",
                "...",
                "No lo sé, amigo, pruébalo tú mismo..."
            }
        };
        dialogueTranslations["Spanish"]["villager6"] = new DialogueTranslation
        {
            name = "Villager 0",
            sentences = new string[]
            {
                "....",
                "Este pilar gigante de aquí es la tumba del rey...",
                "Nadie pudo encontrarlo durante el tiempo de necesidad de los guardianes... Muchos creen que está muerto... Así que construyeron esta tumba en su honor...",
                "¿Yo? Creo que él también está muerto... El rey amaba a su pueblo... Me niego a creer que nos abandonara voluntariamente cuando más lo necesitábamos..."
            }
        };
        dialogueTranslations["Spanish"]["villager7"] = new DialogueTranslation
        {
            name = "Villager 1",
            sentences = new string[]
            {
                "En este pueblo vivían posaderos...",
                "Pero todos se extinguieron...",
                "Ahora solo estoy yo...",
                "Me siento muy solo... A veces tomo piedras, las decoro y les dibujo caras... Ahora son mis únicos amigos..."
            }
        };





        dialogueTranslations["French"]["jeff"] = new DialogueTranslation
        {
            name = "Jeff",
            sentences = new string[]
            {
                "Sacrés haricots ! Un Aubergiste ! Comment est-ce possible ?! Je pensais qu'ils avaient disparu !",
                "Oh, pardonnez mes manières, je m'appelle Jeff ! Je suis résident ici. Ma vie a été bouleversée après la noyade, mais je m'en sors !",
                "Si vous voulez en savoir plus sur ce monde, faites-le moi savoir !"
            }
        };
        dialogueTranslations["French"]["joe"] = new DialogueTranslation
        {
            name = "Joe",
            sentences = new string[]
            {
                "....?",
                "Tu m'as déjà vu ?",
                "Ohhh, tu dois parler de mon frère jumeau Jeff !",
                "Ouais, je déteste ce type. On s'est disputés il y a quelques semaines, alors j'ai déménagé."
            }
        };
        dialogueTranslations["French"]["jay"] = new DialogueTranslation
        {
            name = "Jay",
            sentences = new string[]
            {
                "...",
                "Je viens ici chaque année pour pleurer ma femme...",
                "J'habite dans le village à gauche d'ici...",
                "Ça n'a pas été facile, mais ça s'améliore petit à petit..."
            }
        };  
        dialogueTranslations["French"]["villager1"] = new DialogueTranslation
        {
            name = "Villager 1",
            sentences = new string[]
            {
                "Si vous arrivez au droit d'ici, tenemos un cementerio para recordar a algunos de los ancianos fallecidos de la aldea.", 

                "Aunque nunca hemos tenido un posadero viviendo dans ce pueblo. Dicen que si un posadero muere, la tinta negra empieza a flotar in el lugar donde murió.", 

                "Cuenta la leyenda qu'un posadero murió aquí; nous avons construit un pozo de los deseos en el mismo lugar donde murió."
            }
        };
        dialogueTranslations["French"]["villager2"] = new DialogueTranslation
        {
            name = "Villager 2",
            sentences = new string[]
            {
                "Les intendants avaient un chef qu'ils appelaient leur roi.",
                "Mais un jour, alors que les intendants mouraient, le roi était introuvable.",
                "Nombreux sont ceux qui l'ont cherché, mais tous ont échoué."
            }
        };
        dialogueTranslations["French"]["villager3"] = new DialogueTranslation
        {
            name = "Villager 2",
            sentences = new string[]
            {
                "...", 
                "J'ai appelé ma prof 'maman' aujourd'hui....", 
                "Je ne pourrai jamais me remettre d'un tel niveau d'embarras.....", 
                "Tous les enfants se moquaient de moi...."
            }
        };
        dialogueTranslations["French"]["villager4"] = new DialogueTranslation
        {
            name = "Villager 1",
            sentences = new string[]
            {
                " La légende raconte que les aubergistes ont construit des structures cachées qui sont enterrées ici… ",
                " Les archéologues ont essayé de les chercher, mais personne n'a rien trouvé… ",
                " Je me demande où ils peuvent bien être… ",
                " Peut-être ont-ils besoin d'une sorte de clé… ? Comme un livre… ? "
            }
        };
        dialogueTranslations["French"]["villager5"] = new DialogueTranslation
        {
            name = "Villager 3",
            sentences = new string[]
            {
                "Apparemment, si tu appuies deux fois sur « E » à côté de cette fontaine, tu peux sauvegarder ta progression. ",
                "Qu'est-ce que c'est ? Comment je le sais ?",
                " … ",
                " Je ne sais pas, mec, essaie par toi-même… "
            }
        };
        dialogueTranslations["French"]["villager6"] = new DialogueTranslation
        {
            name = "Villager 0",
            sentences = new string[]
            {
                "....",
                "Ce pilier géant, là, est le tombeau du roi… ",
                "Personne n'a pu le retrouver pendant la période difficile des gardiens… Beaucoup pensent qu'il est mort… Alors, ils ont construit ce tombeau en son honneur… ",
                "Moi personnellement ? Je pense qu'il est mort aussi… Le roi aimait son peuple… Je refuse de croire qu'il nous aurait quittés volontairement au moment où nous avions le plus besoin de lui… "
            }
        };
        dialogueTranslations["French"]["villager7"] = new DialogueTranslation
        {
            name = "Villager 1",
            sentences = new string[]
            {
                "Il y avait des aubergistes dans ce village… ",
                " Mais ils ont tous disparu… ",
                " Maintenant, il n'y a plus que moi… ",
                " Je suis très seul… Parfois, je prends des pierres, je les décore et je dessine des visages dessus… Ce sont mes seuls amis maintenant… "
            }
        };


        
        // Old Book dialogue
        dialogueTranslations["English"]["old_book"] = new DialogueTranslation
        {
            name = "Old Book",
            sentences = new string[]
            {
                "...",
                "It seems to be a diary.",
                "It reads \"Password: It is both a question and an answer. The question is 'Why did the....'\"",
                "You are unable to decipher the end of the sentence....",
                "The diary goes on.",
                "\"The king has ordered me to build this beacon. Decades of my service have finally paid off. I will see to it that this is the greatest piece of architecture an Inkeeper has ever made...\"",
                "You flip the pages to another entry",
                "\"Progress has been grim... Several of us have taken ill... We don't know what the illness is.. But we grow fewer in number with each passing moon...\"",
                "You flip the pages to another entry",
                "\"We have lost too many of our brethren...\"",
                "\"To my dismay it is my turn to part ways with this world as well....\" ",
                "\"The king must be warned... Someone must go to the capital...\"",
                "\"Before it's too late.\""
            }
        };
        
        dialogueTranslations["Spanish"]["old_book"] = new DialogueTranslation
        {
            name = "Libro Viejo",
            sentences = new string[]
            {
                "...",
                "Parece ser un diario.",
                "Dice \"Contraseña: Es tanto una pregunta como una respuesta. La pregunta es '¿Por qué...?'\"",
                "No puedes descifrar el final de la oración....",
                "El diario continúa.",
                "\"El rey me ha ordenado construir este faro. Décadas de mi servicio finalmente han dado fruto. Me aseguraré de que esta sea la mayor obra arquitectónica que un Posadero haya hecho jamás...\"",
                "Pasas las páginas a otra entrada",
                "\"El progreso ha sido sombrío... Varios de nosotros han enfermado... No sabemos qué es la enfermedad... Pero somos menos con cada luna que pasa...\"",
                "Pasas las páginas a otra entrada",
                "\"Hemos perdido demasiados de nuestros hermanos...\"",
                "\"Para mi pesar, es mi turno de partir de este mundo también....\"",
                "\"El rey debe ser advertido... Alguien debe ir a la capital...\"",
                "\"Antes de que sea demasiado tarde.\""
            }
        };
        
        dialogueTranslations["French"]["old_book"] = new DialogueTranslation
        {
            name = "Vieux Livre",
            sentences = new string[]
            {
                "...",
                "Il semble être un journal.",
                "Il dit \"Mot de passe : C'est à la fois une question et une réponse. La question est 'Pourquoi...?'\"",
                "Vous ne pouvez pas déchiffrer la fin de la phrase....",
                "Le journal continue.",
                "\"Le roi m'a ordonné de construire ce phare. Des décennies de mon service ont finalement porté leurs fruits. Je veillerai à ce que ce soit la plus grande œuvre architecturale qu'un Aubergiste ait jamais réalisée...\"",
                "Vous tournez les pages vers une autre entrée",
                "\"Les progrès ont été sombres... Plusieurs d'entre nous sont tombés malades... Nous ne savons pas ce qu'est la maladie... Mais nous diminuons en nombre à chaque lune qui passe...\"",
                "Vous tournez les pages vers une autre entrée",
                "\"Nous avons perdu trop de nos frères...\"",
                "\"À mon grand regret, c'est mon tour de quitter ce monde aussi....\"",
                "\"Le roi doit être prévenu... Quelqu'un doit aller à la capitale...\"",
                "\"Avant qu'il ne soit trop tard.\""
            }
        };
        
        if (enableDebugLogs)
        {
            Debug.Log($"LanguageManager: Loaded dialogue translations for {dialogueTranslations.Count} languages");
            foreach (var lang in dialogueTranslations.Keys)
            {
                Debug.Log($"  - {lang}: {dialogueTranslations[lang].Count} dialogue entries");
            }
        }
    }
    
    public void ChangeLanguage(string newLanguage)
    {
        if (translations.ContainsKey(newLanguage))
        {
            string oldLanguage = currentLanguage;
            currentLanguage = newLanguage;
            SaveLanguage();
            UpdateAllTexts();
            UpdateAllDialogues();
            
            if (enableDebugLogs)
            {
                Debug.Log($"<color=green>Language changed from {oldLanguage} to {newLanguage}</color>");
                Debug.Log($"<color=cyan>Updated {localizedTexts.Count} text components and {localizedDialogues.Count} dialogue components</color>");
            }
        }
        else
        {
            Debug.LogWarning($"Language not supported: {newLanguage}");
        }
    }
    
    public string GetTranslation(string key)
    {
        if (translations.ContainsKey(currentLanguage) && translations[currentLanguage].ContainsKey(key))
        {
            string translation = translations[currentLanguage][key];
            if (enableDebugLogs && showRegisteredTexts)
                Debug.Log($"Translation '{key}' → '{translation}' ({currentLanguage})");
            return translation;
        }
        
        // Fallback to English if current language doesn't have the key
        if (translations.ContainsKey("English") && translations["English"].ContainsKey(key))
        {
            string fallback = translations["English"][key];
            if (enableDebugLogs)
                Debug.LogWarning($"Using English fallback for '{key}': '{fallback}'");
            return fallback;
        }
        
        // If all else fails, return the key itself
        Debug.LogWarning($"Translation not found for key: '{key}' - returning key as fallback");
        return key;
    }
    
    public DialogueTranslation GetDialogueTranslation(string dialogueKey)
    {
        if (dialogueTranslations.ContainsKey(currentLanguage) && dialogueTranslations[currentLanguage].ContainsKey(dialogueKey))
        {
            DialogueTranslation translation = dialogueTranslations[currentLanguage][dialogueKey];
            if (enableDebugLogs && showRegisteredTexts)
                Debug.Log($"Dialogue translation '{dialogueKey}' → '{translation.name}' with {translation.sentences.Length} sentences ({currentLanguage})");
            return translation;
        }
        
        // Fallback to English if current language doesn't have the dialogue key
        if (dialogueTranslations.ContainsKey("English") && dialogueTranslations["English"].ContainsKey(dialogueKey))
        {
            DialogueTranslation fallback = dialogueTranslations["English"][dialogueKey];
            if (enableDebugLogs)
                Debug.LogWarning($"Using English fallback for dialogue '{dialogueKey}': '{fallback.name}'");
            return fallback;
        }
        
        // If all else fails, return null
        Debug.LogWarning($"Dialogue translation not found for key: '{dialogueKey}'");
        return null;
    }
    
    public void RegisterLocalizedText(LocalizedText localizedText)
    {
        if (!localizedTexts.Contains(localizedText))
        {
            localizedTexts.Add(localizedText);
            
            if (enableDebugLogs && showRegisteredTexts)
                Debug.Log($"<color=yellow>Registered LocalizedText: {localizedText.name} (key: '{localizedText.translationKey}')</color>");
        }
    }
    
    public void UnregisterLocalizedText(LocalizedText localizedText)
    {
        if (localizedTexts.Contains(localizedText))
        {
            localizedTexts.Remove(localizedText);
            
            if (enableDebugLogs && showRegisteredTexts)
                Debug.Log($"<color=orange>Unregistered LocalizedText: {localizedText.name}</color>");
        }
    }
    
    public void RegisterLocalizedDialogue(LocalizedDialogue localizedDialogue)
    {
        if (!localizedDialogues.Contains(localizedDialogue))
        {
            localizedDialogues.Add(localizedDialogue);
            
            if (enableDebugLogs && showRegisteredTexts)
                Debug.Log($"<color=yellow>Registered LocalizedDialogue: {localizedDialogue.name} (key: '{localizedDialogue.dialogueKey}')</color>");
        }
    }
    
    public void UnregisterLocalizedDialogue(LocalizedDialogue localizedDialogue)
    {
        if (localizedDialogues.Contains(localizedDialogue))
        {
            localizedDialogues.Remove(localizedDialogue);
            
            if (enableDebugLogs && showRegisteredTexts)
                Debug.Log($"<color=orange>Unregistered LocalizedDialogue: {localizedDialogue.name}</color>");
        }
    }
    
    private void UpdateAllTexts()
    {
        int updatedCount = 0;
        foreach (LocalizedText localizedText in localizedTexts)
        {
            if (localizedText != null)
            {
                localizedText.UpdateText();
                updatedCount++;
            }
        }
        
        if (enableDebugLogs)
            Debug.Log($"<color=cyan>Updated {updatedCount} text components to {currentLanguage}</color>");
    }
    
    private void UpdateAllDialogues()
    {
        int updatedCount = 0;
        foreach (LocalizedDialogue localizedDialogue in localizedDialogues)
        {
            if (localizedDialogue != null)
            {
                localizedDialogue.UpdateDialogue();
                updatedCount++;
            }
        }
        
        if (enableDebugLogs)
            Debug.Log($"<color=cyan>Updated {updatedCount} dialogue components to {currentLanguage}</color>");
    }
    
    private void SaveLanguage()
    {
        PlayerPrefs.SetString("Language", currentLanguage);
        PlayerPrefs.Save();
        
        if (enableDebugLogs)
            Debug.Log($"Saved language preference: {currentLanguage}");
    }
    
    private void LoadSavedLanguage()
    {
        string savedLanguage = PlayerPrefs.GetString("Language", "English");
        currentLanguage = savedLanguage;
        
        if (enableDebugLogs)
            Debug.Log($"Loaded saved language: {currentLanguage}");
    }
    
    // Methods for UI buttons to call
    public void SetLanguageToEnglish()
    {
        ChangeLanguage("English");
    }
    
    public void SetLanguageToSpanish()
    {
        ChangeLanguage("Spanish");
    }
    
    public void SetLanguageToFrench()
    {
        ChangeLanguage("French");
    }
    
    // Debug methods you can call from inspector or console
    [ContextMenu("Debug: Print Current Language")]
    public void DebugPrintCurrentLanguage()
    {
        Debug.Log($"Current Language: {currentLanguage}");
    }
    
    [ContextMenu("Debug: Print Registered Texts")]
    public void DebugPrintRegisteredTexts()
    {
        Debug.Log($"Registered LocalizedText components: {localizedTexts.Count}");
        for (int i = 0; i < localizedTexts.Count; i++)
        {
            if (localizedTexts[i] != null)
                Debug.Log($"  {i + 1}. {localizedTexts[i].name} - Key: '{localizedTexts[i].translationKey}'");
        }
    }
    
    [ContextMenu("Debug: Print Registered Dialogues")]
    public void DebugPrintRegisteredDialogues()
    {
        Debug.Log($"Registered LocalizedDialogue components: {localizedDialogues.Count}");
        for (int i = 0; i < localizedDialogues.Count; i++)
        {
            if (localizedDialogues[i] != null)
                Debug.Log($"  {i + 1}. {localizedDialogues[i].name} - Key: '{localizedDialogues[i].dialogueKey}'");
        }
    }
    
    [ContextMenu("Debug: Test All Languages")]
    public void DebugTestAllLanguages()
    {
        StartCoroutine(TestAllLanguagesCoroutine());
    }
    
    private IEnumerator TestAllLanguagesCoroutine()
    {
        string originalLanguage = currentLanguage;
        
        foreach (string language in translations.Keys)
        {
            Debug.Log($"--- Testing {language} ---");
            ChangeLanguage(language);
            yield return new WaitForSeconds(1f);
        }
        
        // Restore original language
        ChangeLanguage(originalLanguage);
    }
}

[System.Serializable]
public class DialogueTranslation
{
    public string name;
    public string[] sentences;
    public string[] quest_check;
    public string[] quest_complete;
    public string[] choices;
    
    public DialogueTranslation()
    {
        quest_check = new string[0];
        quest_complete = new string[0];
        choices = new string[0];
    }
} 