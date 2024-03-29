using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PseudoRandom : MonoBehaviour
{
    public static PseudoRandom instance;
    private string[] names;

    private void Awake()
    {
        instance = this;
    }

    public string GetRandomPseudo()
    {
        //Source from Chat GPT: give me a list of 300 usernames for internet forum and video games. make the username of maximum 8 character of length. send the list in an array. don't repeat twice the same. vary the length, sometimes add lower or uppercases, sometimes add numbers or underscore
        names = new string[] {
    "StarK1ll",
  "N1nj4Cat",
  "GamerX5",
  "Bolt_12",
  "F1reFly",
  "IceW0lf",
  "Swift_7",
  "Ne0nGuy",
  "Z4pper",
  "BlueM0on",
  "Dr4gon",
  "R4ven",
  "Lucky_9",
  "Tr1cky",
  "Z3phyr",
  "R0ck3t",
  "Frosty",
  "V1rus",
  "MaxP0wer",
  "J3di",
  "N1ghtOwl",
  "Cr4zy_8",
  "Bl4ckJ4ck",
  "Ech0",
  "R4zor",
  "J4ck4l",
  "H4ze",
  "K1ngPin",
  "SlyFox",
  "V3nom_6",
  "D4rkH0rse",
  "W1z4rd",
  "Sn4ke_7",
  "Qu33nBee",
  "T1ger",
  "N1ghtSh4de",
  "B4dG1rl",
  "Skull_5",
  "G0ld3n",
  "Blu3Jay",
  "Chaos_8",
  "J4zzM4n",
  "F1ash",
  "Ghost_9",
  "R0gue",
  "L1ghtning",
  "R4pid",
  "V1per",
  "T3chN1nja",
  "C4pt4in",
  "Moon_6",
  "D4zzle",
  "W1nd",
  "Z3nM4ster",
  "N3tW0rk",
  "Cr1mson",
  "Bl4ze_7",
  "Ch1p",
  "S1lent_8",
  "El3ctra",
  "R3x",
  "G4m3rGuy",
  "Myst1c_5",
  "B0ltz",
  "F1x3r",
  "H4wk_9",
  "K1llJ0y",
  "Lum1nous",
  "N1n3t4il",
  "C0sm1c_6",
  "Blu3_2",
  "D4rthV",
  "R0ck3r_3",
  "W1tch",
  "V0rt3x",
  "T1tan_4",
  "J3st3r",
  "C4k3",
  "Z3us",
  "N1ght_7",
  "B4r0n",
  "Lynx_2",
  "R1pple",
  "F1sh",
  "G4m3On",
  "M4sk_9",
  "B0unc3",
  "S0und",
  "Ch1ll_6",
  "El1t3_3",
  "R4v3n_4",
  "W1z4rd_7",
  "N3xus",
  "C1tad3l",
  "Bl4ck_5",
  "Dr3am_8",
  "Fl4sh_9",
  "H3llion",
  "K1ng_2",
  "L4ser",
  "N0v4",
  "C0bra_7",
  "Blu3_4",
  "D3m0n_6",
  "W1nd0w",
  "V4nd4l",
  "T1ckT0ck",
  "J4zz",
  "C3ph3r",
  "Z3r0_8",
  "N3bula",
  "R0gue_7",
  "W1z4rd_5",
  "P4nther",
  "Cycl0n3",
  "B4rn4cl3",
  "G4l4xy_9",
  "Fl4r3",
  "H3lix",
  "K1ng_6",
  "Lynx_7",
  "R4v3n_3",
  "D3lta",
  "F0x_4",
  "S1lver_8",
  "C0sm0",
  "Bl4ckH4wk",
  "J4ck4l_9",
  "Z3st",
  "N3r0",
  "R0y4l",
  "V3n0m_5",
  "T1ckl3",
  "Ch1p_2",
  "El3ktra",
  "W1tch_6",
  "N3xus_3",
  "Bl4ck_9",
  "Dr3am",
  "Fl4sh_6",
  "H3ro_5",
  "K1ck3r",
  "L4z3r_8",
  "N1ght_2",
  "C0balt",
  "B4s3",
  "G4m3_3",
  "Myst1c_9",
  "B0lt",
  "F1x_6",
  "S1r3n",
  "H4wk_3",
  "K1ngF1sh",
  "Lum1n4",
  "N1n3t4il_8",
  "Ch1m3r4",
  "Blu3_5",
  "Dr4g0n",
  "Fl0w",
  "J4gg3r",
  "R0ck_8",
  "W1z4rd_9",
  "C1t1z3n",
  "N3tW0rk_6",
  "C0d3",
  "Bl4ze",
  "Dr4g0n_3",
  "F1x1t",
  "H4wk_7",
  "K1ng_9",
  "Lynx_5",
  "R4v3n_6",
  "D3m0n",
  "Fl4r3_8",
  "J4zz_3",
  "R0v3r",
  "V4nd4l_6",
  "T1d3",
  "N3tw0rk_2",
  "C0unt",
  "Blu3_7",
  "Dr34m",
  "F1x3r_9",
  "H4wk_4",
  "K1t3",
  "Lynx_3",
  "R4v3n_9",
  "D3m0n_7",
  "F0rt",
  "G4l4xy_5",
  "Fl4r3_4",
  "J4zz_9",
  "R0v3r_6",
  "T1m3",
  "N3xus_2",
  "C0sm1c",
  "Bl4ck_8",
  "Dr3am_3",
  "F1x3r_7",
  "H4wk_5",
  "K1ll3r",
  "Lynx_9",
  "R4v3n_5",
  "D3m0n_4",
  "Fl0w3r",
  "G4m3r_8",
  "Fl4r3_6",
  "J4zz_4",
  "R0v3r_9",
  "T1m3_2",
  "N3xus_7",
  "C0sm1c_5",
  "Bl4ck_6",
  "Dr3am_4",
  "F1x3r_3",
  "H4wk_2",
  "K1ss3r",
  "Lynx_8",
  "R4v3n_7",
  "D3m0n_2",
  "Fl0w_9",
  "G4m3r_7",
  "Fl4r3_5",
  "J4zz_5",
  "R0v3r_4",
  "T1m3_7",
  "N3xus_6",
  "C0sm1c_4",
  "Bl4ck_7",
  "Dr3am_5",
  "F1x3r_4",
  "H4wk_6",
  "K1tt3n",
  "Lynx_4",
  "R4v3n_8",
  "D3m0n_9",
  "Fl0w_4",
  "G4m3r_6",
  "Fl4r3_3",
  "J4zz_8",
  "R0v3r_5",
  "T1m3_9",
  "N3xus_4",
  "C0sm1c_9",
  "Bl4ck_4",
  "Dr3am_6",
  "F1x3r_8",
  "H4wk_8",
  "K1ng_4",
  "Lynx_6",
  "R4v3n_4",
  "D3m0n_3",
  "Fl0w_6",
  "G4m3r_5",
  "Fl4r3_2",
  "J4zz_6",
  "R0v3r_3",
  "T1m3_4",
  "N3xus_5",
  "C0sm1c_3",
  "Bl4ck_3",
  "Dr3am_7",
  "F1x3r_5",
  "H4wk_6",
  "K1ng_5",
  "Lynx_5",
  "R4v3n_5",
  "D3m0n_5",
  "Fl0w_5",
  "G4m3r_5",
  "Fl4r3_5",
  "J4zz_5",
  "R0v3r_5",
  "T1m3_5",
  "N3xus_5",
  "C0sm1c_5",
  "Bl4ck_5",
  "Dr3am_5",
  "F1x3r_5",
  "H4wk_5",
  "K1ng_5",
  "Lynx_5",
  "R4v3n_5",
  "D3m0n_5",
  "Fl0w_5",
  "G4m3r_5",
  "Fl4r3_5",
  "J4zz_5",
  "R0v3r_5",
  "T1m3_5",
  "N3xus_5",
  "C0sm1c_5",
  "Bl4ck_5",
  "Dr3am_5",
  "F1x3r_5",
  "H4wk_5",
  "K1ng_5",
  "Lynx_5",
  "R4v3n_5",
  "D3m0n_5",
  "Fl0w_5",
  "G4m3r_5",
  "Fl4r3_5",
  "J4zz_5",
  "R0v3r_5",
  "T1m3_5",
  "N3xus_5",
  "C0sm1c_5",
  "Bl4ck_5",
  "Dr3am_5",
  "F1x3r_5",
  "H4wk_5",
  "K1ng_5",
  "Lynx_5",
  "R4v3n_5",
  "D3m0n_5",
  "Fl0w_5",
  "G4m3r_5",
  "Fl4r3_5",
  "J4zz_5",
  "R0v3r_5",
  "T1m3_5",
  "N3xus_5",
  "C0sm1c_5",
  "Bl4ck_5",
  "Dr3am_5",
  "F1x3r_5",
  "H4wk_5",
  "K1ng_5",
  "Lynx_5",
  "R4v3n_5",
  "D3m0n_5",
  "Fl0w_5",
  "G4m3r_5",
  "Fl4r3_5",
  "J4zz_5",
  "R0v3r_5",
  "T1m3_5",
  "N3xus_5",
  "C0sm1c_5",
  "Bl4ck_5",
  "Dr3am_5",
  "F1x3r_5",
  "H4wk_5",
  "K1ng_5",
  "Lynx_5",
  "R4v3n_5",
  "D3m0n_5",
  "Fl0w_5",
  "G4m3r_5",
  "Fl4r3_5",
  "J4zz_5",
  "R0v3r_5",
  "T1m3_5",
  "N3xus_5",
  "C0sm1c_5",
  "Bl4ck_5",
  "Dr3am_5",
  "F1x3r_5"};

        int randomIndex = Random.Range(0, names.Length);
        string randomName = names[randomIndex];
        return randomName;
    }
}
