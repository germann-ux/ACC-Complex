from __future__ import annotations

import html
import re
from pathlib import Path
from urllib.parse import unquote


ROOT = Path(__file__).resolve().parent
SOURCE_MD = ROOT / "GUIA_USUARIO_ACC.md"
OUTPUT_HTML = ROOT / "GUIA_USUARIO_ACC.html"

IMAGE_RE = re.compile(r"^!\[(?P<alt>.*?)]\((?P<path>.*?)\)\s*$")
ORDERED_RE = re.compile(r"^(?P<num>\d+)\.\s+(?P<text>.+)$")
UNORDERED_RE = re.compile(r"^-\s+(?P<text>.+)$")
INLINE_CODE_RE = re.compile(r"`([^`]+)`")


def inline_markup(text: str) -> str:
    escaped = html.escape(text)
    return INLINE_CODE_RE.sub(lambda m: f"<code>{html.escape(m.group(1))}</code>", escaped)


def build_html():
    lines = SOURCE_MD.read_text(encoding="utf-8").splitlines()
    body: list[str] = []
    in_ul = False
    in_ol = False

    def close_lists():
        nonlocal in_ul, in_ol
        if in_ul:
            body.append("</ul>")
            in_ul = False
        if in_ol:
            body.append("</ol>")
            in_ol = False

    for raw in lines:
        line = raw.strip()
        if not line:
            close_lists()
            continue

        image_match = IMAGE_RE.match(line)
        if image_match:
            close_lists()
            alt = html.escape(image_match.group("alt"))
            path = unquote(image_match.group("path"))
            body.append(
                f'<figure><img src="{html.escape(path)}" alt="{alt}"><figcaption>{alt}</figcaption></figure>'
            )
            continue

        if line.startswith("### "):
            close_lists()
            body.append(f"<h3>{inline_markup(line[4:])}</h3>")
            continue

        if line.startswith("## "):
            close_lists()
            body.append(f"<h2>{inline_markup(line[3:])}</h2>")
            continue

        if line.startswith("# "):
            close_lists()
            body.append(f"<h1>{inline_markup(line[2:])}</h1>")
            continue

        ordered_match = ORDERED_RE.match(line)
        if ordered_match:
            if in_ul:
                body.append("</ul>")
                in_ul = False
            if not in_ol:
                body.append("<ol>")
                in_ol = True
            body.append(f"<li>{inline_markup(ordered_match.group('text'))}</li>")
            continue

        unordered_match = UNORDERED_RE.match(line)
        if unordered_match:
            if in_ol:
                body.append("</ol>")
                in_ol = False
            if not in_ul:
                body.append("<ul>")
                in_ul = True
            body.append(f"<li>{inline_markup(unordered_match.group('text'))}</li>")
            continue

        close_lists()
        body.append(f"<p>{inline_markup(line)}</p>")

    close_lists()

    html_doc = f"""<!DOCTYPE html>
<html lang="es">
<head>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <title>GUIA USUARIO ACC</title>
  <style>
    body {{
      font-family: "Times New Roman", serif;
      font-size: 12pt;
      color: #000;
      margin: 40px auto;
      max-width: 900px;
      line-height: 1.35;
      text-align: justify;
      background: #fff;
    }}
    h1, h2, h3 {{
      font-weight: 700;
      color: #000;
      text-align: left;
      margin-top: 24px;
      margin-bottom: 10px;
    }}
    p, li {{
      margin-bottom: 8px;
    }}
    ul, ol {{
      margin-bottom: 10px;
    }}
    code {{
      font-family: "Courier New", monospace;
      background: #f3f3f3;
      padding: 1px 4px;
    }}
    figure {{
      margin: 18px 0 26px;
      text-align: center;
    }}
    img {{
      max-width: 100%;
      height: auto;
      border: 1px solid #bbb;
    }}
    figcaption {{
      margin-top: 6px;
      font-size: 11pt;
      text-align: center;
    }}
  </style>
</head>
<body>
{''.join(body)}
</body>
</html>
"""

    OUTPUT_HTML.write_text(html_doc, encoding="utf-8")


if __name__ == "__main__":
    build_html()
